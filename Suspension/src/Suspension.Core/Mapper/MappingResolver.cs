using MudRunner.Commons.DataContracts.Models.Profiles;
using MudRunner.Suspension.Core.Models.NumericalMethod;
using MudRunner.Suspension.Core.Models.SuspensionComponents;
using MudRunner.Suspension.DataContracts.CalculateReactions;
using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic;
using MudRunner.Suspension.DataContracts.RunAnalysis.Static;

namespace MudRunner.Suspension.Core.Mapper
{
    /// <summary>
    /// It is responsible to map an object to another.
    /// </summary>
    public class MappingResolver : IMappingResolver
    {
        /// <inheritdoc/>
        public SuspensionSystem MapFrom(CalculateReactionsRequest request)
        {
            if (request == null)
                return null;

            return new()
            {
                ShockAbsorber = ShockAbsorber.Create(request.ShockAbsorber),
                LowerWishbone = Wishbone.Create(request.LowerWishbone),
                UpperWishbone = Wishbone.Create(request.UpperWishbone),
                TieRod = TieRod.Create(request.TieRod)
            };
        }

        /// <inheritdoc/>
        public SuspensionSystem<TProfile> MapFrom<TProfile>(RunStaticAnalysisRequest<TProfile> runStaticAnalysisRequest, CalculateReactionsResponseData calculateReactionsResponseData)
            where TProfile : Profile
        {
            if (runStaticAnalysisRequest == null)
                return null;

            return new()
            {
                ShockAbsorber = ShockAbsorber.Create(runStaticAnalysisRequest.ShockAbsorber, calculateReactionsResponseData.ShockAbsorberReaction.AbsolutValue),
                LowerWishbone = Wishbone<TProfile>.Create(runStaticAnalysisRequest.LowerWishbone, runStaticAnalysisRequest.Material, calculateReactionsResponseData.LowerWishboneReaction1.AbsolutValue, calculateReactionsResponseData.LowerWishboneReaction2.AbsolutValue),
                UpperWishbone = Wishbone<TProfile>.Create(runStaticAnalysisRequest.UpperWishbone, runStaticAnalysisRequest.Material, calculateReactionsResponseData.UpperWishboneReaction1.AbsolutValue, calculateReactionsResponseData.UpperWishboneReaction2.AbsolutValue),
                TieRod = TieRod<TProfile>.Create(runStaticAnalysisRequest.TieRod, runStaticAnalysisRequest.Material, calculateReactionsResponseData.TieRodReaction.AbsolutValue)
            };
        }

        /// <inheritdoc/>
        public DynamicAnalysisResult MapFrom(NumericalMethodResult numericalMethodResult)
        {
            if (numericalMethodResult == null)
                return null;

            return new()
            {
                Displacement = numericalMethodResult.Displacement.Clone() as double[],
                Velocity = numericalMethodResult.Velocity.Clone() as double[],
                Acceleration = numericalMethodResult.Acceleration.Clone() as double[],
                EquivalentForce = numericalMethodResult.EquivalentForce.Clone() as double[]
            };
        }
    }
}
