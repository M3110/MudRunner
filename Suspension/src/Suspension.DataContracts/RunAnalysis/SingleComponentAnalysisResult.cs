using System;

namespace MudRunner.Suspension.DataContracts.RunAnalysis
{
    /// <summary>
    /// It contains the essential analysis results for a suspension single component.
    /// </summary>
    public class SingleComponentAnalysisResult
    {
        /// <summary>
        /// The analysis safety factor.
        /// Unit: Dimensionless.
        /// </summary>
        public virtual double SafetyFactor => Math.Min(StressSafetyFactor, BucklingSafetyFactor);

        /// <summary>
        /// The applied force.
        /// Unit: N (Newton).
        /// </summary>
        public double AppliedForce { get; set; }

        /// <summary>
        /// The buckling safety factor.
        /// Unit: Dimensionless.
        /// </summary>
        public double BucklingSafetyFactor { get; set; }

        /// <summary>
        /// The stress calculated using Von-Misses method.
        /// Unit: MPa (Megapascal).
        /// </summary>
        public double EquivalentStress { get; set; }

        /// <summary>
        /// The Von-Misses equivalent stress safety factor.
        /// Unit: Dimensionless.
        /// </summary>
        public double StressSafetyFactor { get; set; }
    }
}