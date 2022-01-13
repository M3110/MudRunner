using MudRunner.Commons.Core.Operation;
using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic;
using System.Threading.Tasks;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Dynamic
{
    /// <summary>
    /// It is responsible to run the dynamic analysis to suspension system.
    /// </summary>
    public class RunDynamicAnalysis : OperationBase<RunDynamicAnalysisRequest, RunDynamicAnalysisResponse>, IRunDynamicAnalysis
    {
        protected override Task<RunDynamicAnalysisResponse> ProcessOperationAsync(RunDynamicAnalysisRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
