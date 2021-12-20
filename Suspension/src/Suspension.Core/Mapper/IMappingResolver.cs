using Suspension.Core.Models.SuspensionComponents;
using Suspension.DataContracts.CalculateReactions;
using Suspension.DataContracts.Models.Profiles;
using Suspension.DataContracts.RunAnalysis.Static;

namespace Suspension.Core.Mapper
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