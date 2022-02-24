using MudRunner.Commons.DataContracts.Operation;
using System.Collections.Generic;

namespace MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic
{
    /// <summary>
    /// It represents the 'data' content of RunAmplitudeDynamicAnalysis operation response.
    /// </summary>
    public class RunAmplitudeDynamicAnalysisResponseData : OperationResponseData
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public RunAmplitudeDynamicAnalysisResponseData()
        {
            this.FullFileNames = new List<string>();
        }

        /// <summary>
        /// The full name of solution files.
        /// </summary>
        public List<string> FullFileNames { get; set; }
    }
}
