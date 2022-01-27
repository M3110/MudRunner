using System.Globalization;

namespace MudRunner.Suspension.Core.Models.NumericalMethod
{
    /// <summary>
    /// It contains the finite element analysis results to a specific time.
    /// </summary>
    public class NumericalMethodResult
    {
        /// <summary>
        /// Unit: s (second).
        /// </summary>
        public double Time { get; set; }

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

            return $"{this.Time}," +
                $"{string.Join(",", this.Displacement)}," +
                $"{string.Join(",", this.Velocity)}," +
                $"{string.Join(",", this.Acceleration)}," +
                $"{string.Join(",", this.EquivalentForce)}";
        }
    }
}
