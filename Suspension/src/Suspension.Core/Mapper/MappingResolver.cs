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
        /// This method craetes a <see cref="SuspensionWishbone{TProfile}"/> based on <see cref="RunStaticAnalysisRequest{TProfile}"/> and <see cref="CalculateReactionsResponseData"/>.
        /// </summary>
        /// <typeparam name="TProfile"></typeparam>
        /// <param name="runStaticAnalysisRequest"></param>
        /// <param name="calculateReactionsResponseData"></param>
        /// <returns></returns>
        public SuspensionSystem<TProfile> MapFrom<TProfile>(RunStaticAnalysisRequest<TProfile> runStaticAnalysisRequest, CalculateReactionsResponseData calculateReactionsResponseData)
            where TProfile : Profile
        {
            if (runStaticAnalysisRequest == null)
            {
                return null;
            }

            return new SuspensionSystem<TProfile>
            {
                ShockAbsorber = ShockAbsorber.Create(runStaticAnalysisRequest.ShockAbsorber, calculateReactionsResponseData.ShockAbsorberReaction.AbsolutValue),
                LowerWishbone = SuspensionWishbone<TProfile>.Create(runStaticAnalysisRequest.LowerWishbone, runStaticAnalysisRequest.Material, calculateReactionsResponseData.LowerWishboneReaction1.AbsolutValue, calculateReactionsResponseData.LowerWishboneReaction2.AbsolutValue),
                UpperWishbone = SuspensionWishbone<TProfile>.Create(runStaticAnalysisRequest.UpperWishbone, runStaticAnalysisRequest.Material, calculateReactionsResponseData.UpperWishboneReaction1.AbsolutValue, calculateReactionsResponseData.UpperWishboneReaction2.AbsolutValue),
                TieRod = TieRod<TProfile>.Create(runStaticAnalysisRequest.TieRod, runStaticAnalysisRequest.Material, calculateReactionsResponseData.TieRodReaction.AbsolutValue)
            };
        }
    }
}
