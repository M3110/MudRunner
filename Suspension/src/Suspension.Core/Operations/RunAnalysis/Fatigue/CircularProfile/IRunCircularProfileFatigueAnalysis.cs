using DataContract = MudRunner.Commons.DataContracts.Models.Profiles;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Fatigue.CircularProfile
{
    /// <summary>
    /// It is responsible to run the fatigue analysis to suspension system considering circular profile.
    /// </summary>
    public interface IRunCircularProfileFatigueAnalysis : IRunFatigueAnalysis<DataContract.CircularProfile> { }
}