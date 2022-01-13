using DataContract = MudRunner.Commons.DataContracts.Models.Profiles;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Static.CircularProfile
{
    /// <summary>
    /// It is responsible to run the static analysis to suspension system considering circular profile.
    /// </summary>
    public interface IRunCircularProfileStaticAnalysis : IRunStaticAnalysis<DataContract.CircularProfile> { }
}