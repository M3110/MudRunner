using System.Globalization;

namespace MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic
{
    /// <summary>
    /// It contains the resutls for dynamic analysis.
    /// </summary>
    public class DynamicAnalysisResult
    {
        /// <summary>
        /// Unit: m (meter).
        /// </summary>
        public double[] Displacement { get; set; }

        /// <summary>
        /// Unit: m/s (meter per second).
        /// </summary>
        public double[] Velocity { get; set; }

        /// <summary>
        /// Unit: m/s² (meter per squared second).
        /// </summary>
        public double[] Acceleration { get; set; }

        /// <summary>
        /// Unit: N (Newton).
        /// </summary>
        public double[] EquivalentForce { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            return $"{string.Join(',', this.Displacement)}" +
                $",{string.Join(',', this.Velocity)}" +
                $",{string.Join(',', this.Acceleration)}" +
                $",{string.Join(',', this.EquivalentForce)}";
        }
    }
}
