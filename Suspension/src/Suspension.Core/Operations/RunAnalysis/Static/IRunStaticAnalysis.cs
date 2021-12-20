using Suspension.Core.Models.SuspensionComponents;
using Suspension.Core.Operations.Base;
using Suspension.DataContracts.CalculateReactions;
using Suspension.DataContracts.Models;
using Suspension.DataContracts.Models.Profiles;
using Suspension.DataContracts.RunAnalysis.Static;
using System.Threading.Tasks;

namespace Suspension.Core.Operations.RunAnalysis
{
    /// <summary>
    /// It is responsible to run the static analysis to suspension system.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public interface IRunStaticAnalysis<TProfile> : IOperationBase<RunStaticAnalysisRequest<TProfile>, RunStaticAnalysisResponse> 
        where TProfile : Profile
    {
        /// <summary>
        /// This method builds <see cref="CalculateReactionsRequest"/> based on <see cref="RunStaticAnalysisRequest{TProfile}"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        CalculateReactionsRequest BuildCalculateReactionsRequest(RunStaticAnalysisRequest<TProfile> request);

        /// <summary>
        /// Asynchronously, this method generates the analysis result to shock absorber.
        /// </summary>
        /// <param name="shockAbsorberReaction"></param>
        /// <param name="shouldRoundResults"></param>
        /// <returns></returns>
        Task<Force> GenerateShockAbsorberResultAsync(Force shockAbsorberReaction, bool shouldRoundResults, int numberOfDecimalsToRound);

        /// <summary>
        /// Asynchronously, this method generates the analysis result to single component.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="shouldRoundResults"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        Task<SingleComponentStaticAnalysisResult> GenerateSingleComponentResultAsync(SingleComponent<TProfile> component, bool shouldRoundResults, int decimals = 0);

        /// <summary>
        /// Asynchronously, this method generates the analysis result to suspension A-arm.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="shouldRoundResults"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        Task<SuspensionAArmStaticAnalysisResult> GenerateSuspensionAArmResultAsync(SuspensionAArm<TProfile> component, bool shouldRoundResults, int decimals = 0);
    }
}
