using MudRunner.Commons.Core.ExtensionMethods;
using MudRunner.Commons.Core.Models;
using MudRunner.Suspension.Core.Models.NumericalMethod;

namespace MudRunner.Suspension.Core.NumericalMethods.DifferentialEquation.Newmark
{
    /// <summary>
    /// It is responsible to execute the Newmark numerical method to solve Differential Equation.
    /// </summary>
    public class NewmarkMethod : DifferentialEquationMethod, INewmarkMethod
    {
        /// <inheritdoc/>
        protected override double Gama => (double)1 / 2;

        /// <inheritdoc/>
        protected override double Beta => (double)1 / 6;

        /// <inheritdoc/>
        public override async Task<NumericalMethodResult> CalculateResultAsync(NumericalMethodInput input, NumericalMethodResult previousResult, double time)
        {
            if (time < Constants.InitialTime)
                throw new ArgumentOutOfRangeException(nameof(time), $"The time cannot be less than the initial time: {Constants.InitialTime}.");

            if (time == Constants.InitialTime)
                return this.CalculateInitialResult(input);

            #region Step 1 - Asynchronously, calculates the equivalent stiffness and equivalent force.
            var tasks = new List<Task>();

            double[,] inversedEquivalentStiffness = null;
            tasks.Add(Task.Run(() => inversedEquivalentStiffness = this.CalculateEquivalentStiffness(input).InverseMatrix()));

            double[] equivalentForce = null;
            tasks.Add(Task.Run(async () => equivalentForce = await this.CalculateEquivalentForceAsync(input, previousResult.Displacement, previousResult.Velocity, previousResult.Acceleration)));

            await Task.WhenAll(tasks).ConfigureAwait(false);
            #endregion

            #region Step 2 - Calculates the displacement.
            double[] displacement = inversedEquivalentStiffness.Multiply(equivalentForce);
            #endregion

            #region Step 3 - Calculates the velocity and acceleration.
            double[] velocity = new double[input.NumberOfBoundaryConditions];
            double[] acceleration = new double[input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                acceleration[i] = GetA0(input.TimeStep) * (displacement[i] - previousResult.Displacement[i]) - GetA2(input.TimeStep) * previousResult.Velocity[i] - GetA3() * previousResult.Acceleration[i];
                velocity[i] = previousResult.Velocity[i] + GetA6(input.TimeStep) * previousResult.Acceleration[i] + GetA7(input.TimeStep) * acceleration[i];
            }
            #endregion

            return new NumericalMethodResult
            {
                Time = time,
                Displacement = displacement,
                Velocity = velocity,
                Acceleration = acceleration,
                EquivalentForce = input.EquivalentForce
            };
        }

        /// <summary>
        /// Calculates the equivalent stiffness to calculate the displacement in Newmark method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private double[,] CalculateEquivalentStiffness(NumericalMethodInput input)
        {
            double[,] equivalentStiffness = new double[input.NumberOfBoundaryConditions, input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                for (int j = 0; j < input.NumberOfBoundaryConditions; j++)
                {
                    equivalentStiffness[i, j] = GetA0(input.TimeStep) * input.Mass[i, j] + GetA1(input.TimeStep) * input.Damping[i, j] + input.Stiffness[i, j];
                }
            }

            return equivalentStiffness;
        }

        /// <summary>
        /// Asynchronously, this method calculates the equivalent force to calculate the displacement to Newmark method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousDisplacement"></param>
        /// <param name="previousVelocity"></param>
        /// <param name="previousAcceleration"></param>
        /// <returns></returns>
        private async Task<double[]> CalculateEquivalentForceAsync(NumericalMethodInput input, double[] previousDisplacement, double[] previousVelocity, double[] previousAcceleration)
        {
            #region Calculates the equivalent velocity and equivalent acceleration.
            var tasks = new List<Task>();

            double[] equivalentVelocity = null;
            tasks.Add(Task.Run(() => equivalentVelocity = this.CalculateEquivalentVelocity(input, previousDisplacement, previousVelocity, previousAcceleration)));

            double[] equivalentAcceleration = null;
            tasks.Add(Task.Run(() => equivalentAcceleration = this.CalculateEquivalentAcceleration(input, previousDisplacement, previousVelocity, previousAcceleration)));

            await Task.WhenAll(tasks).ConfigureAwait(false);
            #endregion

            #region Calculates the equivalent forces.
            var forceTasks = new List<Task>();

            double[] equivalentDampingForce = null;
            tasks.Add(Task.Run(() => equivalentDampingForce = input.Damping.Multiply(equivalentVelocity)));

            double[] equivalentDynamicForce = null;
            tasks.Add(Task.Run(() => equivalentDynamicForce = input.Mass.Multiply(equivalentAcceleration)));

            await Task.WhenAll(forceTasks).ConfigureAwait(false);
            #endregion

            return input.EquivalentForce.Sum(equivalentDampingForce, equivalentDynamicForce);
        }

        /// <summary>
        /// Calculates the equivalent aceleration to calculate the equivalent force.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousDisplacement"></param>
        /// <param name="previousVelocity"></param>
        /// <param name="previousAcceleration"></param>
        /// <returns></returns>
        private double[] CalculateEquivalentAcceleration(NumericalMethodInput input, double[] previousDisplacement, double[] previousVelocity, double[] previousAcceleration)
        {
            double[] equivalentAcceleration = new double[input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                equivalentAcceleration[i] = GetA0(input.TimeStep) * previousDisplacement[i] + GetA2(input.TimeStep) * previousVelocity[i] + GetA3() * previousAcceleration[i];
            }

            return equivalentAcceleration;
        }

        /// <summary>
        /// Calculates the equivalent velocity to calculate the equivalent force.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousDisplacement"></param>
        /// <param name="previousVelocity"></param>
        /// <param name="previousAcceleration"></param>
        /// <returns></returns>
        private double[] CalculateEquivalentVelocity(NumericalMethodInput input, double[] previousDisplacement, double[] previousVelocity, double[] previousAcceleration)
        {
            double[] equivalentVelocity = new double[input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                equivalentVelocity[i] = GetA1(input.TimeStep) * previousDisplacement[i] + GetA4() * previousVelocity[i] + GetA5(input.TimeStep) * previousAcceleration[i];
            }

            return equivalentVelocity;
        }

        #region Integration Constants

        private double GetA0(double timeStep) => 1 / (this.Beta * Math.Pow(timeStep, 2));

        private double GetA1(double timeStep) => this.Gama / (this.Beta * timeStep);

        private double GetA2(double timeStep) => 1 / (this.Beta * timeStep);

        private double GetA3() => 1 / (2 * this.Beta) - 1;

        private double GetA4() => this.Gama / this.Beta - 1;

        private double GetA5(double timeStep) => timeStep / 2 * (this.Gama / this.Beta - 2);

        private double GetA6(double timeStep) => timeStep * (1 - this.Gama);

        private double GetA7(double timeStep) => this.Gama * timeStep;

        #endregion
    }
}
