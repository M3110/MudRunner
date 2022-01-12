using MudRunner.Commons.Core.ConstitutiveEquations.Fatigue;
using MudRunner.Suspension.Core.Operations.RunAnalysis.Static.CircularProfile;
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
    }
}
