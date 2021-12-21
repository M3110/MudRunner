using MudRunner.Commons.DataContracts.Models.Profiles;
using MudRunner.Suspension.Core.Models.SuspensionComponents;
using MudRunner.Suspension.DataContracts.CalculateReactions;
using MudRunner.Suspension.DataContracts.RunAnalysis.Static;

namespace MudRunner.Suspension.Core.Mapper
{
    /// <summary>
    /// It is responsible to map an object to another.
    /// </summary>
    public interface IMappingResolver
    {
        /// <summary>
        /// This method creates a <see cref="SuspensionSystem"/> based on <see cref="CalculateReactionsRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SuspensionSystem MapFrom(CalculateReactionsRequest request);

        /// <summary>
        /// This method craetes a <see cref="SuspensionAArm{TProfile}"/> based on <see cref="RunStaticAnalysisRequest{TProfile}"/> and <see cref="CalculateReactionsResponseData"/>.
        /// </summary>
        /// <typeparam name="TProfile"></typeparam>
        /// <param name="runAnalysisRequest"></param>
        /// <param name="calculateReactionsResponseData"></param>
        /// <returns></returns>
        SuspensionSystem<TProfile> MapFrom<TProfile>(RunStaticAnalysisRequest<TProfile> runAnalysisRequest, CalculateReactionsResponseData calculateReactionsResponseData)
            where TProfile : Profile;
    }
}