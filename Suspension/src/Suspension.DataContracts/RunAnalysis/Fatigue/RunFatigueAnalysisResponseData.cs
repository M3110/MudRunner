using MudRunner.Suspension.DataContracts.OperationBase;
using System.Collections.Generic;
using System.Linq;

namespace MudRunner.Suspension.DataContracts.RunAnalysis.Fatigue
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
        /// The fatigue safety factor.
        /// </summary>
        public double FatigueSafetyFactor => new List<double>
        {
            (SuspensionAArmUpperResult?.FatigueSafetyFactor).GetValueOrDefault(),
            (SuspensionAArmLowerResult?.FatigueSafetyFactor).GetValueOrDefault(),
            (TieRodResult?.FatigueSafetyFactor).GetValueOrDefault()
        }.Min();

        /// <summary>
        /// The fatigue number of cycles.
        /// </summary>
        public double FatigueNumberOfCycles => new List<double>
        {
            (SuspensionAArmUpperResult?.FatigueNumberOfCycles).GetValueOrDefault(),
            (SuspensionAArmLowerResult?.FatigueNumberOfCycles).GetValueOrDefault(),
            (TieRodResult?.FatigueNumberOfCycles).GetValueOrDefault()
        }.Min();

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
