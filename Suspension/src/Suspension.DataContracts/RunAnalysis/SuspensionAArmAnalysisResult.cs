using System;

namespace Suspension.DataContracts.RunAnalysis
{
    /// <summary>
    /// It contains the essential analysis results for a suspension A-arm.
    /// </summary>
    public abstract class SuspensionAArmAnalysisResult<TSingleComponent>
        where TSingleComponent : SingleComponentAnalysisResult
    {
        /// <summary>
        /// The analysis safety factor.
        /// Unit: Dimensionless.
        /// </summary>
        public double SafetyFactor => Math.Min(FirstSegment.SafetyFactor, SecondSegment.SafetyFactor);

        /// <summary>
        /// The Von-Misses equivalent stress safety factor.
        /// Unit: Dimensionless.
        /// </summary>
        public double StressSafetyFactor => Math.Min(FirstSegment.StressSafetyFactor, SecondSegment.StressSafetyFactor);

        /// <summary>
        /// The buckling safety factor.
        /// Unit: Dimensionless.
        /// </summary>
        public double BucklingSafetyFactor => Math.Min(FirstSegment.BucklingSafetyFactor, SecondSegment.BucklingSafetyFactor);

        /// <summary>
        /// The results for the first segment of suspension A-arm.
        /// </summary>
        public TSingleComponent FirstSegment { get;set; }

        /// <summary>
        /// The results for the second segment of suspension A-arm.
        /// </summary>
        public TSingleComponent SecondSegment { get; set; }
    }
}
