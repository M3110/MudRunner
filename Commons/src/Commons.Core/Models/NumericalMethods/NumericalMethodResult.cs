using System.Globalization;

namespace MudRunner.Suspension.Core.Models.NumericalMethod
{
    /// <summary>
    /// It contains the finite element analysis results to a specific time.
    /// </summary>
    public class NumericalMethodResult
    {
        /// <summary>
        /// Basic constructor.
        /// </summary>
        public NumericalMethodResult() { }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="numberOfBoundaryConditions"></param>
        public NumericalMethodResult(uint numberOfBoundaryConditions)
        {
            this.Displacement = new double[numberOfBoundaryConditions];
            this.Velocity = new double[numberOfBoundaryConditions];
            this.Acceleration = new double[numberOfBoundaryConditions];
            this.EquivalentForce = new double[numberOfBoundaryConditions];
        }

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
