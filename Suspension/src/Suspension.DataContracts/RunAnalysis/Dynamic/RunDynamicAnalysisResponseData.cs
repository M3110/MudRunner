using MudRunner.Commons.DataContracts.Operation;
using System.Collections.Generic;

namespace MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic
{
    /// <summary>
    /// It represents the 'data' content of RunDynamicAnalysis operation response.
    /// </summary>
    public class RunDynamicAnalysisResponseData : OperationResponseData
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public RunDynamicAnalysisResponseData()
        {
            this.FullFileNames = new List<string>();
        }

        /// <summary>
        /// The full name of solution file.
        /// </summary>
        // TODO: Disponibilizar os arquivos para download.
        public List<string> FullFileNames { get; set; }

        /// <summary>
        /// The maximum result for analysis.
        /// </summary>
        public DynamicAnalysisResult MaximumResult { get; set; }

        /// <summary>
        /// The maximum deformation result.
        /// </summary>
        public DynamicAnalysisResult MaximumDeformationResult { get; set; }
    }
}
