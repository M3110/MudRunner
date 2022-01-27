using MudRunner.Suspension.Core.Models.NumericalMethod;

namespace MudRunner.Suspension.Core.NumericalMethods.DifferentialEquation
{
    /// <summary>
    /// It is responsible to execute numerical method to solve Differential Equation.
    /// </summary>
    public abstract class DifferentialEquationMethod : IDifferentialEquationMethod
    {
        /// <summary>
        /// Dimensionless.
        /// </summary>
        protected abstract double Gama { get; }

        /// <summary>
        /// Dimensionless.
        /// </summary>
        protected abstract double Beta { get; }

        /// <inheritdoc/>
        public virtual NumericalMethodResult CalculateInitialResult(NumericalMethodInput input)
        {
            return new NumericalMethodResult
            {
                Displacement = new double[input.NumberOfBoundaryConditions],
                Velocity = new double[input.NumberOfBoundaryConditions],
                Acceleration = new double[input.NumberOfBoundaryConditions],
                EquivalentForce = input.EquivalentForce
            };
        }

        /// <inheritdoc/>
        public abstract Task<NumericalMethodResult> CalculateResultAsync(NumericalMethodInput input, NumericalMethodResult previousResult, double time);
    }
}
