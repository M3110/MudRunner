using MudRunner.Commons.DataContracts.Models;
using MudRunner.Commons.DataContracts.Operation;
using System.Collections.Generic;
using System.Linq;

namespace MudRunner.Suspension.DataContracts.RunAnalysis
{
    /// <summary>
    /// It represents the 'data' content of RunAnalysis operation response.
    /// </summary>
    public class RunAnalysisResponseData : OperationResponseData
    {
        /// <summary>
        /// True, if analysis failed. False, otherwise.
        /// </summary>
        public bool AnalisysFailed => SafetyFactor < 1;

        /// <summary>
        /// The safety factor.
        /// </summary>
        public double SafetyFactor
            => new List<double> { this.UpperWishboneResult.SafetyFactor, this.LowerWishboneResult.SafetyFactor, this.TieRod.SafetyFactor }.Min();

        /// <summary>
        /// The force reactions at shock absorber.
        /// </summary>
        public Force ShockAbsorber { get; set; }

        /// <summary>
        /// The analysis result to suspension upper wishbone.
        /// </summary>
        public SuspensionWishboneAnalysisResult UpperWishboneResult { get; set; }

        /// <summary>
        /// The analysis result to suspension lower wishbone.
        /// </summary>
        public SuspensionWishboneAnalysisResult LowerWishboneResult { get; set; }

        /// <summary>
        /// The analysis result to tie rod.
        /// </summary>
        public SingleComponentAnalysisResult TieRod { get; set; }
    }
}
