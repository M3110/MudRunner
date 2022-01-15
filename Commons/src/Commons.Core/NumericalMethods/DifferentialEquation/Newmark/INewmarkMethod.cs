using MudRunner.Suspension.Core.Models.NumericalMethod.Newmark;

namespace MudRunner.Suspension.Core.NumericalMethods.DifferentialEquation.Newmark
{
    /// <summary>
    /// It is responsible to execute the Newmark numerical method to solve Differential Equation.
    /// </summary>
    public interface INewmarkMethod : IDifferentialEquationMethod<NewmarkMethodInput> { }
}
