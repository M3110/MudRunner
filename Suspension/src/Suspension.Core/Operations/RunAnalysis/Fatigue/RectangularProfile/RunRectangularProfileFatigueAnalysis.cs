using MudRunner.Suspension.Core.ConstitutiveEquations.Fatigue;
using MudRunner.Suspension.Core.Operations.RunAnalysis.Static.RectangularProfile;
using DataContract = MudRunner.Suspension.DataContracts.Models.Profiles;

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
    }
}
