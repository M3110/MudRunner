using Suspension.Core.ConstitutiveEquations.Fatigue;
using Suspension.Core.Operations.RunAnalysis.Static.CircularProfile;
using DataContract = Suspension.DataContracts.Models.Profiles;

namespace Suspension.Core.Operations.RunAnalysis.Fatigue.CircularProfile
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
