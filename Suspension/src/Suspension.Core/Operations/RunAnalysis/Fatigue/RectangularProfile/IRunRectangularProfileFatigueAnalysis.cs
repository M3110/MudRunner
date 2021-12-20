using DataContract = Suspension.DataContracts.Models.Profiles;

namespace Suspension.Core.Operations.RunAnalysis.Fatigue.RectangularProfile
{
    /// <summary>
    /// It is responsible to run the fatigue analysis to suspension system considering rectangular profile.
    /// </summary>
    public interface IRunRectangularProfileFatigueAnalysis : IRunFatigueAnalysis<DataContract.RectangularProfile> { }
}