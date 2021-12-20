using Suspension.DataContracts.Models;
using Suspension.DataContracts.OperationBase;
using System.Collections.Generic;
using System.Linq;

namespace Suspension.DataContracts.RunAnalysis.Fatigue
{
    /// <summary>
    /// It represents the 'data' content of RunFatigueAnalysis operation response.
    /// </summary>
    public class RunFatigueAnalysisResponseData : OperationResponseData
    {
        /// <summary>
        /// True, if analysis failed. False, otherwise.
        /// </summary>
        public bool AnalisysFailed => SafetyFactor < 1;

        /// <summary>
        /// The safety factor.
        /// </summary>
        public double SafetyFactor => new List<double> { SuspensionAArmUpperResult.SafetyFactor, SuspensionAArmLowerResult.SafetyFactor, TieRodResult.SafetyFactor }.Min();

        /// <summary>
        /// The force reactions at shock absorber.
        /// </summary>
        public ShockAbsorberFatigueAnalysisResult ShockAbsorberResult { get; set; }

        /// <summary>
        /// The analysis result to suspension A-arm upper.
        /// </summary>
        public SuspensionAArmFatigueAnalysisResult SuspensionAArmUpperResult { get; set; }

        /// <summary>
        /// The analysis result to suspension A-arm lower.
        /// </summary>
        public SuspensionAArmFatigueAnalysisResult SuspensionAArmLowerResult { get; set; }

        /// <summary>
        /// The analysis result to tie rod.
        /// </summary>
        public SingleComponentFatigueAnalysisResult TieRodResult { get; set; }
    }
}
