using DataContract = MudRunner.Suspension.DataContracts.Models.Profiles;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Static.RectangularProfile
{
    /// <summary>
    /// It is responsible to run the static analysis to suspension system considering rectangular profile.
    /// </summary>
    public interface IRunRectangularProfileStaticAnalysis : IRunStaticAnalysis<DataContract.RectangularProfile> { }
}