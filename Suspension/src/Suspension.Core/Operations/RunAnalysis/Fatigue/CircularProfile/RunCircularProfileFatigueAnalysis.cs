using MudRunner.Commons.Core.ConstitutiveEquations.Fatigue;
using MudRunner.Commons.DataContracts.Operation;
using MudRunner.Suspension.Core.Operations.RunAnalysis.Static.CircularProfile;
using MudRunner.Suspension.DataContracts.RunAnalysis.Fatigue;
using System.Threading.Tasks;
using DataContract = MudRunner.Commons.DataContracts.Models.Profiles;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Fatigue.CircularProfile
{
    /// <summary>
    /// It is responsible to run the fatigue analysis to suspension system considering circular profile.
    /// </summary>
    public class RunCircularProfileFatigueAnalysis : RunFatigueAnalysis<DataContract.CircularProfile>, IRunCircularProfileFatigueAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="runStaticAnalysis"></param>
        /// <param name="fatigue"></param>
        public RunCircularProfileFatigueAnalysis(
            IRunCircularProfileStaticAnalysis runStaticAnalysis,
            IFatigue<DataContract.CircularProfile> fatigue)
            : base(runStaticAnalysis, fatigue)
        { }

        /// <summary>
        /// Asynchronously, this method validates the <see cref="RunFatigueAnalysisRequest{CircularProfile}"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<OperationResponse<RunFatigueAnalysisResponseData>> ValidateOperationAsync(RunFatigueAnalysisRequest<DataContract.CircularProfile> request)
        {
            OperationResponse<RunFatigueAnalysisResponseData> response = new();
            response.SetSuccessOk();

            return Task.FromResult(response);
        }
    }
}
