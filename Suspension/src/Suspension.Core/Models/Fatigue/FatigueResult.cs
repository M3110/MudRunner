namespace Suspension.Core.Models.Fatigue
{
    /// <summary>
    /// It contains the result for fatigue analysis.
    /// </summary>
    public class FatigueResult
    {
        /// <summary>
        /// The fatigue safety factor based on Modified Goodman.
        /// Dimensionless.
        /// </summary>
        public double SafetyFactor { get; set; }

        /// <summary>
        /// Unit: MPa (Mega Pascal).
        /// </summary>
        public double StressAmplitude { get; set; }

        /// <summary>
        /// Unit: MPa (Mega Pascal).
        /// </summary>
        public double MeanStress { get; set; }

        /// <summary>
        /// Unit: MPa (Mega Pascal).
        /// </summary>
        public double EquivalentStress { get; set; }

        /// <summary>
        /// Dimensionless.
        /// </summary>
        public double NumberOfCicles { get; set; }
    }
}
