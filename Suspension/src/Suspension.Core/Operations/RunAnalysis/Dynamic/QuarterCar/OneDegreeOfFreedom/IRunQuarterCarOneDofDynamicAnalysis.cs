using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic.QuarterCar.OneDegreeOfFreedom;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Dynamic.QuarterCar.OneDegreeOfFreedom
{
    /// <summary>
    /// It is responsible to run the dynamic analysis to suspension system considering quarter car and one degrees of freedom.
    /// </summary>
    public interface IRunQuarterCarOneDofDynamicAnalysis : IRunDynamicAnalysis<RunQuarterCarOneDofDynamicAnalysisRequest> { }
}
