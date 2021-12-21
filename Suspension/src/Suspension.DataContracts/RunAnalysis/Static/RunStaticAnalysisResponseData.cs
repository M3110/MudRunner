using MudRunner.Suspension.DataContracts.Models;
using MudRunner.Suspension.DataContracts.OperationBase;
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
            (SuspensionAArmUpperResult?.SafetyFactor).GetValueOrDefault(),
            (SuspensionAArmLowerResult?.SafetyFactor).GetValueOrDefault(),
            (TieRodResult?.SafetyFactor).GetValueOrDefault()
        }.Min();

        /// <summary>
        /// The Von-Misses equivalent stress safety factor.
        /// </summary>
        public double StressSafetyFactor => new List<double>
        {
            (SuspensionAArmUpperResult?.StressSafetyFactor).GetValueOrDefault(),
            (SuspensionAArmLowerResult?.StressSafetyFactor).GetValueOrDefault(),
            (TieRodResult?.StressSafetyFactor).GetValueOrDefault()
        }.Min();

        /// <summary>
        /// The buckling safety factor.
        /// </summary>
        public double BucklingSafetyFactor => new List<double>
        {
            (SuspensionAArmUpperResult?.BucklingSafetyFactor).GetValueOrDefault(),
            (SuspensionAArmLowerResult?.BucklingSafetyFactor).GetValueOrDefault(),
            (TieRodResult?.BucklingSafetyFactor).GetValueOrDefault()
        }.Min();

        /// <summary>
        /// The force reactions at shock absorber.
        /// </summary>
        public Force ShockAbsorberResult { get; set; }

        /// <summary>
        /// The analysis result to suspension A-arm upper.
        /// </summary>
        public SuspensionAArmStaticAnalysisResult SuspensionAArmUpperResult { get; set; }

        /// <summary>
        /// The analysis result to suspension A-arm lower.
        /// </summary>
        public SuspensionAArmStaticAnalysisResult SuspensionAArmLowerResult { get; set; }

        /// <summary>
        /// The analysis result to tie rod.
        /// </summary>
        public SingleComponentStaticAnalysisResult TieRodResult { get; set; }
    }
}
