using DataContract = Suspension.DataContracts.Models.Profiles;

namespace Suspension.Core.Operations.RunAnalysis.Static.CircularProfile
{
    /// <summary>
    /// It is responsible to run the static analysis to suspension system considering circular profile.
    /// </summary>
    public interface IRunCircularProfileStaticAnalysis : IRunStaticAnalysis<DataContract.CircularProfile> { }
}