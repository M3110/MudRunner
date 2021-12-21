using System.Collections.Generic;
using System.Linq;

namespace MudRunner.Suspension.DataContracts.RunAnalysis.Fatigue
{
    /// <summary>
    /// It contains the essential fatigue analysis results for a suspension single component.
    /// </summary>
    public class SingleComponentFatigueAnalysisResult : SingleComponentAnalysisResult 
    {
        /// <summary>
        /// The analysis safety factor.
        /// Unit: Dimensionless.
        /// </summary>
        public override double SafetyFactor => new List<double> { StressSafetyFactor, BucklingSafetyFactor, FatigueSafetyFactor }.Min();

        /// <summary>
        /// The equivalent stress at fatigue analysis.
        /// Unit: MPa (Mega Pascal).
        /// </summary>
        public double FatigueEquivalentStress { get; set; }

        /// <summary>
        /// The fatigue safety factor based on Modified Goodman.
        /// Dimensionless.
        /// </summary>
        public double FatigueSafetyFactor { get; set; }

        /// <summary>
        /// The number of cycles at fatigue analysis.
        /// Dimensionless.
        /// </summary>
        public double FatigueNumberOfCycles { get; set; }
    }
}