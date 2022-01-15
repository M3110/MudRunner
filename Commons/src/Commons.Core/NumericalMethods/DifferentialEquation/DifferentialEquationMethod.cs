using MudRunner.Suspension.Core.Models.NumericalMethod;

namespace MudRunner.Suspension.Core.NumericalMethods.DifferentialEquation
{
    /// <summary>
    /// It is responsible to execute numerical method to solve Differential Equation.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    public abstract class DifferentialEquationMethod<TInput> : IDifferentialEquationMethod<TInput>
        where TInput : NumericalMethodInput
    {
        /// <inheritdoc/>
        public virtual NumericalMethodResult CalculateInitialResult(TInput input)
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
        public abstract Task<NumericalMethodResult> CalculateResultAsync(TInput input, NumericalMethodResult previousResult, double time);
    }
}
