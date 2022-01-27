using MudRunner.Commons.DataContracts.Models.Enums;
using MudRunner.Suspension.Core.NumericalMethods.DifferentialEquation;

namespace MudRunner.Commons.Core.Factory.DifferentialEquationMethod
{
    /// <summary>
    /// It is responsible to build a <see cref="DifferentialEquationMethod"/>.
    /// </summary>
    public interface IDifferentialEquationMethodFactory
    {
        /// <summary>
        /// This method gets the numerical method corresponding to <see cref="DifferentialEquationMethodEnum"/>.
        /// </summary>
        /// <param name="differentialEquationMethodEnum"></param>
        /// <returns></returns>
        IDifferentialEquationMethod Get(DifferentialEquationMethodEnum differentialEquationMethodEnum);
    }
}