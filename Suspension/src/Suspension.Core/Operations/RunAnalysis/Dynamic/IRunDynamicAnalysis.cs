using MudRunner.Commons.Core.Operation;
using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Dynamic
{
    /// <summary>
    /// It is responsible to run the dynamic analysis to suspension system.
    /// </summary>
    public interface IRunDynamicAnalysis : IOperationBase<RunDynamicAnalysisRequest, RunDynamicAnalysisResponse>
    {
    }
}