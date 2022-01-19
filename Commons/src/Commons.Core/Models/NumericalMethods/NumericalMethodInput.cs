namespace MudRunner.Suspension.Core.Models.NumericalMethod
{
    /// <summary>
    /// It contains the input data for a numerical method.
    /// </summary>
    public abstract class NumericalMethodInput
    {
        /// <summary>
        /// Unit: s (second).
        /// </summary>
        public double TimeStep { get; set; }

        /// <summary>
        /// Unit: kg (kilogram).
        /// </summary>
        public double[,] Mass { get; set; }

        /// <summary>
        /// Unit: N/m (Newton per meter).
        /// </summary>
        public double[,] Stiffness { get; set; }

        /// <summary>
        /// Unit: N.s/m (Newston-second per meter).
        /// </summary>
        public double[,] Damping { get; set; }

        /// <summary>
        /// Unit: N (Newton).
        /// </summary>
        public double[] EquivalentForce { get; set; }

        /// <summary>
        /// Dimensionless.
        /// </summary>
        public uint NumberOfBoundaryConditions { get; set; }

        /// <summary>
        /// Dimensionless.
        /// </summary>
        public abstract double Gama { get; }

        /// <summary>
        /// Dimensionless.
        /// </summary>
        public abstract double Beta { get; }
    }
}
