using MudRunner.Commons.Core.ConstitutiveEquations.Fatigue;
using MudRunner.Suspension.Core.Operations.RunAnalysis.Static.RectangularProfile;
using MudRunner.Suspension.DataContracts.RunAnalysis.Fatigue;
using System.Threading.Tasks;
using DataContract = MudRunner.Commons.DataContracts.Models.Profiles;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Fatigue.RectangularProfile
{
    /// <summary>
    /// It is responsible to run the fatigue analysis to suspension system considering rectangular profile.
    /// </summary>
    public class RunRectangularProfileFatigueAnalysis : RunFatigueAnalysis<DataContract.RectangularProfile>, IRunRectangularProfileFatigueAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="runStaticAnalysis"></param>
        /// <param name="fatigue"></param>
        public RunRectangularProfileFatigueAnalysis(
            IRunRectangularProfileStaticAnalysis runStaticAnalysis,
            IFatigue<DataContract.RectangularProfile> fatigue) : base(runStaticAnalysis, fatigue)
        { }

        /// <summary>
        /// Asynchronously, this method validates the <see cref="RunFatigueAnalysisRequest{RectangularProfile}"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<RunFatigueAnalysisResponse> ValidateOperationAsync(RunFatigueAnalysisRequest<DataContract.RectangularProfile> request)
        {
            RunFatigueAnalysisResponse response = new();
            response.SetSuccessOk();

            return Task.FromResult(response);
        }
    }
}
