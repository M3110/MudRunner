using MudRunner.Commons.DataContracts.Operation;

namespace MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic
{
    /// <summary>
    /// It represents the 'data' content of RunDynamicAnalysis operation response.
    /// </summary>
    public class RunDynamicAnalysisResponseData : OperationResponseData
    {
        /// <summary>
        /// The full name of solution file.
        /// </summary>
        public string FullFileName { get; set; }

        /// <summary>
        /// The maximum results for analysis.
        /// </summary>
        public DynamicAnalysisResult MaximumResult { get; set; }
    }
}
