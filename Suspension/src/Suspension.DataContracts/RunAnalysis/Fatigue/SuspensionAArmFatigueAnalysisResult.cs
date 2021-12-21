using System;

namespace Suspension.DataContracts.RunAnalysis.Fatigue
{
    /// <summary>
    /// It contains the essential fatigue analysis results for a suspension A-arm.
    /// </summary>
    public class SuspensionAArmFatigueAnalysisResult : SuspensionAArmAnalysisResult<SingleComponentFatigueAnalysisResult> 
    {
        /// <summary>
        /// The fatigue safety factor based on Modified Goodman.
        /// Dimensionless.
        /// </summary>
        public double FatigueSafetyFactor => Math.Min(FirstSegment.FatigueSafetyFactor, SecondSegment.FatigueSafetyFactor);

        /// <summary>
        /// Dimensionless.
        /// </summary>
        public double FatigueNumberOfCycles => Math.Min(FirstSegment.FatigueNumberOfCycles, SecondSegment.FatigueNumberOfCycles);
    }
}
