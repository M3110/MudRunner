using MudRunner.Commons.DataContracts.Models.Profiles;
using MudRunner.Suspension.Core.Models.SuspensionComponents;
using MudRunner.Suspension.DataContracts.CalculateReactions;
using MudRunner.Suspension.DataContracts.RunAnalysis.Static;

namespace MudRunner.Suspension.Core.Mapper
{
    /// <summary>
    /// It is responsible to map an object to another.
    /// </summary>
    public class MappingResolver : IMappingResolver
    {
        /// <summary>
        /// This method creates a <see cref="SuspensionSystem"/> based on <see cref="CalculateReactionsRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SuspensionSystem MapFrom(CalculateReactionsRequest request)
        {
            if (request == null)
            {
                return null;
            }

            return new SuspensionSystem
            {
                ShockAbsorber = ShockAbsorber.Create(request.ShockAbsorber),
                LowerWishbone = SuspensionWishbone.Create(request.LowerWishbone),
                UpperWishbone = SuspensionWishbone.Create(request.UpperWishbone),
                TieRod = TieRod.Create(request.TieRod)
            };
        }

        /// <summary>
        /// This method craetes a <see cref="SuspensionWishbone{TProfile}"/> based on <see cref="RunAnalysisRequest{TProfile}"/> and <see cref="CalculateReactionsResponseData"/>.
        /// </summary>
        /// <typeparam name="TProfile"></typeparam>
        /// <param name="runAnalysisRequest"></param>
        /// <param name="calculateReactionsResponseData"></param>
        /// <returns></returns>
        public SuspensionSystem<TProfile> MapFrom<TProfile>(RunStaticAnalysisRequest<TProfile> runAnalysisRequest, CalculateReactionsResponseData calculateReactionsResponseData)
            where TProfile : Profile
        {
            if (runAnalysisRequest == null)
            {
                return null;
            }

            return new SuspensionSystem<TProfile>
            {
                ShockAbsorber = ShockAbsorber.Create(runAnalysisRequest.ShockAbsorber, calculateReactionsResponseData.ShockAbsorberReaction.AbsolutValue),
                LowerWishbone = SuspensionWishbone<TProfile>.Create(runAnalysisRequest.LowerWishbone, runAnalysisRequest.Material, calculateReactionsResponseData.LowerWishboneReaction1.AbsolutValue, calculateReactionsResponseData.LowerWishboneReaction2.AbsolutValue),
                UpperWishbone = SuspensionWishbone<TProfile>.Create(runAnalysisRequest.UpperWishbone, runAnalysisRequest.Material, calculateReactionsResponseData.UpperWishboneReaction1.AbsolutValue, calculateReactionsResponseData.UpperWishboneReaction2.AbsolutValue),
                TieRod = TieRod<TProfile>.Create(runAnalysisRequest.TieRod, runAnalysisRequest.Material, calculateReactionsResponseData.TieRodReaction.AbsolutValue)
            };
        }
    }
}
