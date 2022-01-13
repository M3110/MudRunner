using MudRunner.Commons.DataContracts.Models;
using MudRunner.Commons.DataContracts.Operation;
using System.Collections.Generic;
using System.Linq;

namespace MudRunner.Suspension.DataContracts.RunAnalysis.Static
{
    /// <summary>
    /// It represents the 'data' content of RunStaticAnalysis operation response.
    /// </summary>
    public class RunStaticAnalysisResponseData : OperationResponseData
    {
        /// <summary>
        /// True, if analysis failed. False, otherwise.
        /// </summary>
        public bool AnalisysFailed => SafetyFactor < 1;

        /// <summary>
        /// The safety factor.
        /// </summary>
        public double SafetyFactor => new List<double>
        {
            (UpperWishboneResult?.SafetyFactor).GetValueOrDefault(),
            (LowerWishboneResult?.SafetyFactor).GetValueOrDefault(),
            (TieRodResult?.SafetyFactor).GetValueOrDefault()
        }.Min();

        /// <summary>
        /// The Von-Misses equivalent stress safety factor.
        /// </summary>
        public double StressSafetyFactor => new List<double>
        {
            (UpperWishboneResult?.StressSafetyFactor).GetValueOrDefault(),
            (LowerWishboneResult?.StressSafetyFactor).GetValueOrDefault(),
            (TieRodResult?.StressSafetyFactor).GetValueOrDefault()
        }.Min();

        /// <summary>
        /// The buckling safety factor.
        /// </summary>
        public double BucklingSafetyFactor => new List<double>
        {
            (UpperWishboneResult?.BucklingSafetyFactor).GetValueOrDefault(),
            (LowerWishboneResult?.BucklingSafetyFactor).GetValueOrDefault(),
            (TieRodResult?.BucklingSafetyFactor).GetValueOrDefault()
        }.Min();

        /// <summary>
        /// The force reactions at shock absorber.
        /// </summary>
        public Force ShockAbsorberResult { get; set; }

        /// <summary>
        /// The analysis result to upper wishbone.
        /// </summary>
        public WishboneStaticAnalysisResult UpperWishboneResult { get; set; }

        /// <summary>
        /// The analysis result to lower wishbone.
        /// </summary>
        public WishboneStaticAnalysisResult LowerWishboneResult { get; set; }

        /// <summary>
        /// The analysis result to tie rod.
        /// </summary>
        public SingleComponentStaticAnalysisResult TieRodResult { get; set; }
    }
}
