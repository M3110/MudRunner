using MudRunner.Commons.DataContracts.Models.Enums;
using MudRunner.Suspension.Core.NumericalMethods.DifferentialEquation;

namespace MudRunner.Commons.Core.Factory.DifferentialEquationMethod
{
    /// <summary>
    /// It is responsible to build a <see cref="DifferentialEquationMethod"/>.
    /// </summary>
    public class DifferentialEquationMethodFactory : IDifferentialEquationMethodFactory
    {
        private readonly IDictionary<DifferentialEquationMethodEnum, IDifferentialEquationMethod> _differentialEquationMethods;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="differentialEquationMethods"></param>
        public DifferentialEquationMethodFactory(IDictionary<DifferentialEquationMethodEnum, IDifferentialEquationMethod> differentialEquationMethods)
        {
            this._differentialEquationMethods = differentialEquationMethods;
        }

        /// <inheritdoc/>
        public IDifferentialEquationMethod Get(DifferentialEquationMethodEnum differentialEquationMethodEnum)
        {
            return this._differentialEquationMethods.FirstOrDefault(d => d.Key == differentialEquationMethodEnum).Value;
        }
    }
}
