using MudRunner.Commons.Core.ConstitutiveEquations.MechanicsOfMaterials;
using MudRunner.Commons.Core.GeometricProperties;
using MudRunner.Commons.Core.Operation;
using MudRunner.Commons.DataContracts.Models;
using MudRunner.Commons.DataContracts.Models.Profiles;
using MudRunner.Suspension.Core.Mapper;
using MudRunner.Suspension.Core.Models.SuspensionComponents;
using MudRunner.Suspension.Core.Operations.CalculateReactions;
using MudRunner.Suspension.DataContracts.CalculateReactions;
using MudRunner.Suspension.DataContracts.Models.SuspensionComponents;
using MudRunner.Suspension.DataContracts.RunAnalysis.Static;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreModels = MudRunner.Suspension.Core.Models.SuspensionComponents;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis
{
    /// <summary>
    /// It is responsible to run the static analysis to suspension system.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class RunStaticAnalysis<TProfile> : OperationBase<RunStaticAnalysisRequest<TProfile>, RunStaticAnalysisResponse>, IRunStaticAnalysis<TProfile>
        where TProfile : Profile
    {
        private readonly ICalculateReactions _calculateReactions;
        private readonly IMechanicsOfMaterials _mechanicsOfMaterials;
        private readonly IGeometricProperty<TProfile> _geometricProperty;
        private readonly IMappingResolver _mappingResolver;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="calculateReactions"></param>
        /// <param name="mechanicsOfMaterials"></param>
        /// <param name="geometricProperty"></param>
        /// <param name="mappingResolver"></param>
        public RunStaticAnalysis(
            ICalculateReactions calculateReactions,
            IMechanicsOfMaterials mechanicsOfMaterials,
            IGeometricProperty<TProfile> geometricProperty,
            IMappingResolver mappingResolver)
        {
            this._calculateReactions = calculateReactions;
            this._mechanicsOfMaterials = mechanicsOfMaterials;
            this._geometricProperty = geometricProperty;
            this._mappingResolver = mappingResolver;
        }

        /// <summary>
        /// Asynchronously, this method runs the static analysis to suspension system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<RunStaticAnalysisResponse> ProcessOperationAsync(RunStaticAnalysisRequest<TProfile> request)
        {
            var response = new RunStaticAnalysisResponse();
            response.SetSuccessOk();

            // Step 1 - Calculates the reactions at suspension system for the applied force.
            CalculateReactionsResponse calculateReactionsResponse = await this._calculateReactions.ProcessAsync(this.BuildCalculateReactionsRequest(request)).ConfigureAwait(false);
            if (calculateReactionsResponse.Success == false)
            {
                response.SetInternalServerError("Occurred error while calculating the reactions to suspension system.");
                response.AddErrors(calculateReactionsResponse.Errors, calculateReactionsResponse.HttpStatusCode);

                return response;
            }

            // Step 2 - Builds the suspension system based on CalculateReaction operation response and request.
            // Here is built the main information to be used at analysis.
            SuspensionSystem<TProfile> suspensionSystem = this._mappingResolver.MapFrom(request, calculateReactionsResponse.Data);

            // Step 3 - Generate the result and maps to response.
            List<Task> tasks = new();

            tasks.Add(Task.Run(async () =>
            {
                response.Data.ShockAbsorberResult = await this.GenerateShockAbsorberResultAsync(calculateReactionsResponse.Data.ShockAbsorberReaction, request.ShouldRoundResults, request.NumberOfDecimalsToRound.GetValueOrDefault()).ConfigureAwait(false);
            }));

            tasks.Add(Task.Run(async () =>
            {
                response.Data.LowerWishboneResult = await this.GenerateWishboneResultAsync(suspensionSystem.LowerWishbone, request.ShouldRoundResults, request.NumberOfDecimalsToRound.GetValueOrDefault()).ConfigureAwait(false);
            }));

            tasks.Add(Task.Run(async () =>
            {
                response.Data.UpperWishboneResult = await this.GenerateWishboneResultAsync(suspensionSystem.UpperWishbone, request.ShouldRoundResults, request.NumberOfDecimalsToRound.GetValueOrDefault()).ConfigureAwait(false);
            }));

            tasks.Add(Task.Run(async () =>
            {
                response.Data.TieRodResult = await this.GenerateSingleComponentResultAsync(suspensionSystem.TieRod, request.ShouldRoundResults, request.NumberOfDecimalsToRound.GetValueOrDefault());
            }));

            await Task.WhenAll(tasks);

            return response;
        }

        /// <summary>
        /// This method builds <see cref="CalculateReactionsRequest"/> based on <see cref="RunStaticAnalysisRequest{TProfile}"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CalculateReactionsRequest BuildCalculateReactionsRequest(RunStaticAnalysisRequest<TProfile> request)
        {
            return new CalculateReactionsRequest
            {
                ShouldRoundResults = false,
                AppliedForce = request.AppliedForce,
                Origin = request.Origin,
                ShockAbsorber = ShockAbsorberPoint.Create(request.ShockAbsorber),
                LowerWishbone = WishbonePoint.Create(request.LowerWishbone),
                UpperWishbone = WishbonePoint.Create(request.UpperWishbone),
                TieRod = TieRodPoint.Create(request.TieRod)
            };
        }

        /// <summary>
        /// Asynchronously, this method generates the analysis result to shock absorber.
        /// </summary>
        /// <param name="shockAbsorberReaction"></param>
        /// <param name="shouldRoundResults"></param>
        /// <param name="numberOfDecimalsToRound"></param>
        /// <returns></returns>
        public Task<Force> GenerateShockAbsorberResultAsync(Force shockAbsorberReaction, bool shouldRoundResults, int numberOfDecimalsToRound)
        {
            if (shouldRoundResults)
                return Task.FromResult(shockAbsorberReaction.Round(numberOfDecimalsToRound));

            return Task.FromResult(shockAbsorberReaction);
        }

        /// <summary>
        /// Asynchronously, this method generates the analysis result to suspension A-arm.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="shouldRoundResults"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public async Task<WishboneStaticAnalysisResult> GenerateWishboneResultAsync(CoreModels.Wishbone<TProfile> component, bool shouldRoundResults, int decimals = 0)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "The object suspension A-arm cannot be null to calculate the results.");

            return new()
            {
                FirstSegment = await GenerateSingleComponentResultAsync(new SingleComponent<TProfile>
                {
                    AppliedForce = component.AppliedForce1,
                    PivotPoint = component.FrontPivot,
                    FasteningPoint = component.OuterBallJoint,
                    Material = component.Material,
                    Profile = component.Profile,
                }, shouldRoundResults, decimals).ConfigureAwait(false),
                SecondSegment = await GenerateSingleComponentResultAsync(new SingleComponent<TProfile>
                {
                    AppliedForce = component.AppliedForce2,
                    PivotPoint = component.RearPivot,
                    FasteningPoint = component.OuterBallJoint,
                    Material = component.Material,
                    Profile = component.Profile,
                }, shouldRoundResults, decimals).ConfigureAwait(false)
            };
        }

        /// <summary>
        /// Asynchronously, this method generates the analysis result to single component.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="shouldRoundResults"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public Task<SingleComponentStaticAnalysisResult> GenerateSingleComponentResultAsync(SingleComponent<TProfile> component, bool shouldRoundResults, int decimals = 0)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "The object tie rod cannot be null to calculate the results.");

            // Step i - Calculates the geometric properties.
            double area = this._geometricProperty.CalculateArea(component.Profile);
            double momentOfInertia = this._geometricProperty.CalculateMomentOfInertia(component.Profile);

            // Step ii - Calculates the equivalent stress.
            // For that case, just is considered the normal stress because the applied force is at same axis of geometry.
            double equivalentStress = this._mechanicsOfMaterials.CalculateNormalStress(component.AppliedForce, area);

            // Step iii - Calculates the critical buckling force.
            double criticalBucklingForce = this._mechanicsOfMaterials.CalculateCriticalBucklingForce(component.Material.YoungModulus, momentOfInertia, component.Length);

            // Step iv - Builds the analysis result.
            SingleComponentStaticAnalysisResult result = new()
            {
                AppliedForce = component.AppliedForce,
                BucklingSafetyFactor = Math.Abs(criticalBucklingForce / component.AppliedForce),
                EquivalentStress = equivalentStress,
                StressSafetyFactor = Math.Abs(component.Material.YieldStrength / equivalentStress)
            };

            // Step v - Rounds the results if it was requested. 
            if (shouldRoundResults)
            {
                result.AppliedForce = Math.Round(result.AppliedForce, decimals);
                result.BucklingSafetyFactor = Math.Round(result.BucklingSafetyFactor, decimals);
                result.EquivalentStress = Math.Round(result.EquivalentStress, decimals);
                result.StressSafetyFactor = Math.Round(result.StressSafetyFactor, decimals);
            }
            
            return Task.FromResult(result);
        }
    }
}
