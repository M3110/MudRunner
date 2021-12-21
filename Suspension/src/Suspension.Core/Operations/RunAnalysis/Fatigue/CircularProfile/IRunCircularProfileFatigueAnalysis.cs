using DataContract = Suspension.DataContracts.Models.Profiles;

namespace Suspension.Core.Operations.RunAnalysis.Fatigue.CircularProfile
{
    /// <summary>
    /// It is responsible to run the fatigue analysis to suspension system considering circular profile.
    /// </summary>
    public interface IRunCircularProfileFatigueAnalysis : IRunFatigueAnalysis<DataContract.CircularProfile> { }
}