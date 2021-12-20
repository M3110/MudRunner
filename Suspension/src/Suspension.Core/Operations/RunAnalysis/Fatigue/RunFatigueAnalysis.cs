using Suspension.Core.ConstitutiveEquations.Fatigue;
using Suspension.Core.Models.Fatigue;
using Suspension.Core.Operations.Base;
using Suspension.DataContracts.Models;
using Suspension.DataContracts.Models.Profiles;
using Suspension.DataContracts.RunAnalysis.Fatigue;
using Suspension.DataContracts.RunAnalysis.Static;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Suspension.Core.Operations.RunAnalysis.Fatigue
{
    /// <summary>
    /// It is responsible to run the fatigue analysis to suspension system.
    /// </summary>
    public abstract class RunFatigueAnalysis<TProfile> : OperationBase<RunFatigueAnalysisRequest<TProfile>, RunFatigueAnalysisResponse>, IRunFatigueAnalysis<TProfile>
        where TProfile : Profile
    {
        private readonly IRunStaticAnalysis<TProfile> _runStaticAnalysis;
        private readonly IFatigue<TProfile> _fatigue;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="runStaticAnalysis"></param>
        public RunFatigueAnalysis(IRunStaticAnalysis<TProfile> runStaticAnalysis, IFatigue<TProfile> fatigue)
        {
            this._runStaticAnalysis = runStaticAnalysis;
            this._fatigue = fatigue;
        }

        /// <summary>
        /// Asynchronously, this method runs the fatigue analysis to suspension system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<RunFatigueAnalysisResponse> ProcessOperationAsync(RunFatigueAnalysisRequest<TProfile> request)
        {
            RunFatigueAnalysisResponse response = new();
            response.SetSuccessOk();

            // Step 1 - Run static analysis for the minimum and maximum force.
            var result = await RunStaticAnalyses(request, response).ConfigureAwait(false);
            if (response.Success == false)
                return response;

            RunStaticAnalysisResponseData runMaximumStaticAnalysisResponseData = result.RunMaximumStaticAnalysisResponseData;
            RunStaticAnalysisResponseData runMinimumStaticAnalysisResponseData = result.RunMinimumStaticAnalysisResponseData;

            // Step 2 - Generate the result and maps to response.
            List<Task> tasks = new();

            //tasks.Add(Task.Run(async () =>
            //{
                response.Data.ShockAbsorberResult = await GenerateShockAbsorberResultAsync(
                    runMaximumStaticAnalysisResponseData.ShockAbsorberResult,
                    runMinimumStaticAnalysisResponseData.ShockAbsorberResult,
                    request.ShouldRoundResults, request.NumberOfDecimalsToRound.GetValueOrDefault()).ConfigureAwait(false);
            //}));

            //tasks.Add(Task.Run(async () =>
            //{
                response.Data.SuspensionAArmLowerResult = await GenerateSuspensionAArmResultAsync(
                    // It is necessary to inform the profile aside for it be used correctly according to the component.
                    request, request.SuspensionAArmLower.Profile,
                    runMaximumStaticAnalysisResponseData.SuspensionAArmLowerResult,
                    runMinimumStaticAnalysisResponseData.SuspensionAArmLowerResult).ConfigureAwait(false);
            //}));

            //tasks.Add(Task.Run(async () =>
            //{
                response.Data.SuspensionAArmUpperResult = await GenerateSuspensionAArmResultAsync(
                    // It is necessary to inform the profile aside for it be used correctly according to the component.
                    request, request.SuspensionAArmUpper.Profile,
                    runMaximumStaticAnalysisResponseData.SuspensionAArmUpperResult,
                    runMinimumStaticAnalysisResponseData.SuspensionAArmUpperResult).ConfigureAwait(false);
            //}));

            //tasks.Add(Task.Run(async () =>
            //{
                response.Data.TieRodResult = await GenerateSingleComponentResultAsync(
                    // It is necessary to inform the profile aside for it be used correctly according to the component.
                    request, request.TieRod.Profile,
                    runMaximumStaticAnalysisResponseData.TieRodResult,
                    runMinimumStaticAnalysisResponseData.TieRodResult);
            //}));

            await Task.WhenAll(tasks);

            return response;
        }

        /// <summary>
        /// Asynchronously, this method runs the static analysis for the minimum and maximum applied force.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task<(RunStaticAnalysisResponseData RunMaximumStaticAnalysisResponseData, RunStaticAnalysisResponseData RunMinimumStaticAnalysisResponseData)> RunStaticAnalyses(RunFatigueAnalysisRequest<TProfile> request, RunFatigueAnalysisResponse response)
        {
            RunStaticAnalysisResponse runMaximumStaticAnalysisResponse = new();
            RunStaticAnalysisResponse runMinimumStaticAnalysisResponse = new();

            List<Task> tasks = new();
            tasks.Add(Task.Run(async () =>
            {
                RunStaticAnalysisRequest<TProfile> runMaximumStaticAnalysisRequest = BuildRunStaticAnalysisRequest(request, request.MaximumAppliedForce);
                runMaximumStaticAnalysisResponse = await _runStaticAnalysis.ProcessAsync(runMaximumStaticAnalysisRequest).ConfigureAwait(false);
            }));
            tasks.Add(Task.Run(async () =>
            {
                RunStaticAnalysisRequest<TProfile> runMinimumStaticAnalysisRequest = BuildRunStaticAnalysisRequest(request, request.MinimumAppliedForce);
                runMinimumStaticAnalysisResponse = await _runStaticAnalysis.ProcessAsync(runMinimumStaticAnalysisRequest).ConfigureAwait(false);
            }));
            await Task.WhenAll(tasks);

            if (runMaximumStaticAnalysisResponse.Success == false)
            {
                response.SetInternalServerError("Occurred error durring the static analysis to suspension system considering the maximum force.");
                response.AddErrors(runMaximumStaticAnalysisResponse.Errors, runMaximumStaticAnalysisResponse.HttpStatusCode);
            }

            if (runMinimumStaticAnalysisResponse.Success == false)
            {
                response.SetInternalServerError("Occurred error durring the static analysis to suspension system considering the minimum force.");
                response.AddErrors(runMinimumStaticAnalysisResponse.Errors, runMinimumStaticAnalysisResponse.HttpStatusCode);
            }

            return (runMaximumStaticAnalysisResponse.Data, runMinimumStaticAnalysisResponse.Data);
        }

        /// <summary>
        /// This method builds the <see cref="RunStaticAnalysisRequest{TProfile}"/> based on <see cref="RunFatigueAnalysisRequest{TProfile}"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="appliedForce"></param>
        /// <returns></returns>
        public RunStaticAnalysisRequest<TProfile> BuildRunStaticAnalysisRequest(RunFatigueAnalysisRequest<TProfile> request, string appliedForce)
        {
            return new()
            {
                AppliedForce = appliedForce,
                Material = request.Material,
                ShouldRoundResults = request.ShouldRoundResults,
                NumberOfDecimalsToRound = request.NumberOfDecimalsToRound,
                Origin = request.Origin,
                ShockAbsorber = request.ShockAbsorber,
                SuspensionAArmLower = request.SuspensionAArmLower,
                SuspensionAArmUpper = request.SuspensionAArmUpper,
                TieRod = request.TieRod
            };
        }

        /// <summary>
        /// Asynchronously, this method generates the result for shock absorber.
        /// </summary>
        /// <param name="shockAbsorberMaximumResult"></param>
        /// <param name="shockAbsorberMinimumResult"></param>
        /// <param name="shouldRoundResults"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public Task<ShockAbsorberFatigueAnalysisResult> GenerateShockAbsorberResultAsync(Force shockAbsorberMaximumResult, Force shockAbsorberMinimumResult, bool shouldRoundResults, int decimals)
        {
            // Force amplitude = Absolut((Maximum force - Minimum Force) / 2)
            Force forceAmplitude = shockAbsorberMaximumResult.Subtract(shockAbsorberMinimumResult).Divide(2).Abs();
            // Mean force = (Maximum force + Minimum Force) / 2
            Force meanForce = shockAbsorberMaximumResult.Sum(shockAbsorberMinimumResult).Divide(2);

            return Task.FromResult(new ShockAbsorberFatigueAnalysisResult
            {
                ForceAmplitude = shouldRoundResults ? forceAmplitude.Round(decimals) : forceAmplitude,
                MeanForce = shouldRoundResults ? meanForce.Round(decimals) : meanForce
            });
        }

        /// <summary>
        /// Asynchronously, this method generates the result for suspension A-arm.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="profile"></param>
        /// <param name="suspensionAArmUpperMaximumResult"></param>
        /// <param name="suspensionAArmUpperMinimumResult"></param>
        /// <returns></returns>
        public async Task<SuspensionAArmFatigueAnalysisResult> GenerateSuspensionAArmResultAsync(RunFatigueAnalysisRequest<TProfile> request, TProfile profile,
            SuspensionAArmStaticAnalysisResult suspensionAArmUpperMaximumResult, SuspensionAArmStaticAnalysisResult suspensionAArmUpperMinimumResult)
        {
            return new()
            {
                FirstSegment = await GenerateSingleComponentResultAsync(
                    request, profile,
                    suspensionAArmUpperMaximumResult.FirstSegment,
                    suspensionAArmUpperMinimumResult.FirstSegment),
                SecondSegment = await GenerateSingleComponentResultAsync(
                    request, profile,
                    suspensionAArmUpperMaximumResult.SecondSegment,
                    suspensionAArmUpperMinimumResult.SecondSegment)
            };
        }

        /// <summary>
        /// Asynchronously, this method generates the result for suspension single component.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="profile"></param>
        /// <param name="singleComponentMaximumResult"></param>
        /// <param name="singleComponentMinimumResult"></param>
        /// <returns></returns>
        public Task<SingleComponentFatigueAnalysisResult> GenerateSingleComponentResultAsync(RunFatigueAnalysisRequest<TProfile> request, TProfile profile,
            SingleComponentStaticAnalysisResult singleComponentMaximumResult, SingleComponentStaticAnalysisResult singleComponentMinimumResult)
        {
            // Step i - Builds the input for fatigue analysis.
            FatigueInput<TProfile> fatigueInput = new()
            {
                FatigueLimit = request.FatigueLimit,
                FatigueLimitFraction = request.FatigueLimitFraction,
                IsRotativeSection = request.IsRotativeSection,
                Reliability = request.Reliability,
                SurfaceFinish = request.SurfaceFinish,
                TensileStress = Material.Create(request.Material).TensileStress,
                Profile = profile,
                MinimumAppliedStress = request.FatigueStressConcentrationFactor * singleComponentMinimumResult.EquivalentStress,
                MaximumAppliedStress = request.FatigueStressConcentrationFactor * singleComponentMaximumResult.EquivalentStress,
                LoadingType = LoadingType.Axial,
            };

            // Step ii - Calculates the results for fatigue analysis.
            FatigueResult fatigueResult = this._fatigue.CalculateFatigueResult(fatigueInput);

            // Step iii - Maps the result for SingleComponentFatigueAnalysisResult.
            SingleComponentFatigueAnalysisResult result = new()
            {
                AppliedForce = GetAbsoluteMaximum(singleComponentMaximumResult.AppliedForce, singleComponentMinimumResult.AppliedForce),
                BucklingSafetyFactor = GetAbsoluteMinimum(singleComponentMaximumResult.BucklingSafetyFactor, singleComponentMinimumResult.BucklingSafetyFactor),
                EquivalentStress = GetAbsoluteMaximum(singleComponentMaximumResult.EquivalentStress, singleComponentMinimumResult.EquivalentStress),
                StressSafetyFactor = GetAbsoluteMinimum(singleComponentMaximumResult.StressSafetyFactor, singleComponentMinimumResult.StressSafetyFactor),
                FatigueEquivalentStress = fatigueResult.EquivalentStress,
                FatigueNumberOfCycles = fatigueResult.NumberOfCycles,
                FatigueSafetyFactor = fatigueResult.SafetyFactor
            };

            // Step iv - Rounds the results if it was requested. 
            if (request.ShouldRoundResults)
            {
                result.AppliedForce = Math.Round(result.AppliedForce, request.NumberOfDecimalsToRound.GetValueOrDefault());
                result.BucklingSafetyFactor = Math.Round(result.BucklingSafetyFactor, request.NumberOfDecimalsToRound.GetValueOrDefault());
                result.EquivalentStress = Math.Round(result.EquivalentStress, request.NumberOfDecimalsToRound.GetValueOrDefault());
                result.StressSafetyFactor = Math.Round(result.StressSafetyFactor, request.NumberOfDecimalsToRound.GetValueOrDefault());
                result.FatigueEquivalentStress = Math.Round(result.FatigueEquivalentStress, request.NumberOfDecimalsToRound.GetValueOrDefault());
                result.FatigueNumberOfCycles = Math.Round(result.FatigueNumberOfCycles, request.NumberOfDecimalsToRound.GetValueOrDefault());
                result.FatigueSafetyFactor = Math.Round(result.FatigueSafetyFactor, request.NumberOfDecimalsToRound.GetValueOrDefault());
            }

            return Task.FromResult(result);
        }

        /// <summary>
        /// This method gets the absolute minimum between two values.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        private double GetAbsoluteMinimum(double value1, double value2)
        {
            return Math.Abs(value1) > Math.Abs(value2) ? value2 : value1;
        }

        /// <summary>
        /// This method gets the absolute maximum between two values.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        private double GetAbsoluteMaximum(double value1, double value2)
        {
            return Math.Abs(value1) > Math.Abs(value2) ? value1 : value2;
        }
    }
}
