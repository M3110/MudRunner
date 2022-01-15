using MudRunner.Suspension.Core.Models.NumericalMethod;

namespace MudRunner.Suspension.Core.NumericalMethods.DifferentialEquation
{
    /// <summary>
    /// It is responsible to execute numerical method to solve Differential Equation
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    public interface IDifferentialEquationMethod<TInput>
        where TInput : NumericalMethodInput
    {
        /// <summary>
        /// Asynchronously, this method calculates the result for the initial time for a matricial analysis.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        NumericalMethodResult CalculateInitialResult(TInput input);

        /// <summary>
        /// Asynchronously, this method calculates the results for a numeric analysis.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousResult"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<NumericalMethodResult> CalculateResultAsync(TInput input, NumericalMethodResult previousResult, double time);
    }
}