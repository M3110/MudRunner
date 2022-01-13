using DataContract = MudRunner.Commons.DataContracts.Models.Profiles;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Fatigue.RectangularProfile
{
    /// <summary>
    /// It is responsible to run the fatigue analysis to suspension system considering rectangular profile.
    /// </summary>
    public interface IRunRectangularProfileFatigueAnalysis : IRunFatigueAnalysis<DataContract.RectangularProfile> { }
}