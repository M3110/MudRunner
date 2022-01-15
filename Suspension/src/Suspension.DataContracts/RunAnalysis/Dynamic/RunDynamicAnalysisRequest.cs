using MudRunner.Commons.DataContracts.Operation;

namespace MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic
{
    /// <summary>
    /// It represents the request content of RunDynamicAnalysis operation.
    /// </summary>
    public class RunDynamicAnalysisRequest : OperationRequestBase
    {
        /// <summary>
        /// Unit: s (second).
        /// </summary>
        public double TimeStep { get; set; }

        /// <summary>
        /// Unit: s (second).
        /// </summary>
        public double FinalTime { get; set; }

        /// <summary>
        /// An additional information to be set in the file name that contains the analysis results.
        /// </summary>
        public string AdditionalFileNameInformation { get; set; }
    }
}
