using MudRunner.Commons.Core.ExtensionMethods;
using MudRunner.Suspension.Core.Models.NumericalMethod;

namespace MudRunner.Suspension.Core.NumericalMethods.DifferentialEquation.NewmarkBeta
{
    /// <summary>
    /// It is responsible to execute the Newmark-Beta numerical method to solve Differential Equation.
    /// </summary>
    public class NewmarkBetaMethod : DifferentialEquationMethod, INewmarkBetaMethod
    {
        /// <inheritdoc/>
        protected override double Gama => (double)1 / 2;

        /// <inheritdoc/>
        protected override double Beta => (double)1 / 6;

        /// <inheritdoc/>
        public override async Task<NumericalMethodResult> CalculateResultAsync(NumericalMethodInput input, NumericalMethodResult previousResult, double time)
        {
            if (time < 0)
                throw new ArgumentOutOfRangeException(nameof(time), "The time cannot be negative.");

            if (time == 0)
                return this.CalculateInitialResult(input);

            #region Step 1 - Asynchronously, calculates the equivalent stiffness and equivalent force.
            List<Task> tasks = new();

            double[,] inversedEquivalentStiffness = null;
            tasks.Add(Task.Run(() => inversedEquivalentStiffness = this.CalculateEquivalentStiffness(input).InverseMatrix()));

            double[] equivalentForce = null;
            tasks.Add(Task.Run(async () => equivalentForce = await this.CalculateEquivalentForceAsync(input, previousResult, time)));

            await Task.WhenAll(tasks).ConfigureAwait(false);
            #endregion

            #region Step 2 - Calculates the displacement.
            double[] deltaDisplacement = inversedEquivalentStiffness.Multiply(equivalentForce);
            double[] displacement = previousResult.Displacement.Sum(deltaDisplacement);
            #endregion

            #region Step 3 - Calculates the velocity.
            double[] velocity = new double[input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                velocity[i] = GetA1(input.TimeStep) * deltaDisplacement[i] + (1 - GetA3()) * previousResult.Velocity[i] - GetA5(input.TimeStep) * previousResult.Acceleration[i];
            }
            #endregion

            #region Step 4 - Calculates the acceleration.
            List<Task> accelerationTasks1 = new();

            double[] damping_velocity = null;
            accelerationTasks1.Add(Task.Run(() => damping_velocity = input.Damping.Multiply(velocity)));

            double[] stiffness_displacement = null;
            accelerationTasks1.Add(Task.Run(() => stiffness_displacement = input.Stiffness.Multiply(displacement)));

            await Task.WhenAll(accelerationTasks1).ConfigureAwait(false);

            List<Task> accelerationTasks2 = new();

            double[] systemEquivalentForce = null;
            accelerationTasks2.Add(Task.Run(() => systemEquivalentForce = input.EquivalentForce.Subtract(damping_velocity).Subtract(stiffness_displacement)));

            double[,] inversedMass = null;
            accelerationTasks2.Add(Task.Run(() => inversedMass = input.Mass.InverseMatrix()));

            await Task.WhenAll(accelerationTasks2).ConfigureAwait(false);

            // [Acceleration] = -inv([M]) * [System Equivalent Force]
            //    [System Equivalent Force] = [Equivalent Force] - [Stiffness] * [Diplacement] - [Damping] * [Velocity]
            double[] acceleration = inversedMass.Multiply(systemEquivalentForce);

            #endregion

            return new()
            {
                Time = time,
                Displacement = displacement,
                Velocity = velocity,
                Acceleration = acceleration,
                EquivalentForce = input.EquivalentForce
            };
        }

        /// <summary>
        /// This method calculates the equivalent stiffness to calculate the displacement in Newmark-Beta method.
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
        /// Asynchronously, this method calculates the equivalent force to calculate the displacement in Newmark-Beta method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousResult"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private async Task<double[]> CalculateEquivalentForceAsync(NumericalMethodInput input, NumericalMethodResult previousResult, double time)
        {
            #region Calculates the equivalent damping and equivalent mass.
            List<Task> tasks = new();

            double[,] equivalentDamping = null;
            tasks.Add(Task.Run(() => equivalentDamping = this.CalculateEquivalentDamping(input)));

            double[,] equivalentMass = null;
            tasks.Add(Task.Run(() => equivalentMass = this.CalculateEquivalentMass(input)));

            await Task.WhenAll(tasks).ConfigureAwait(false);
            #endregion

            #region Calculates the equivalent forces.
            List<Task> forceTasks = new();

            double[] equivalentDampingForce = null;
            forceTasks.Add(Task.Run(() => equivalentDampingForce = equivalentDamping.Multiply(previousResult.Velocity)));

            double[] equivalentDynamicForce = null;
            forceTasks.Add(Task.Run(() => equivalentDynamicForce = equivalentMass.Multiply(previousResult.Acceleration)));

            await Task.WhenAll(forceTasks).ConfigureAwait(false);
            #endregion

            return input.EquivalentForce
                .Subtract(previousResult.EquivalentForce)
                .Sum(equivalentDampingForce, equivalentDynamicForce);
        }

        /// <summary>
        /// This method calculates the equivalent damping to be used in Newmark-Beta method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private double[,] CalculateEquivalentDamping(NumericalMethodInput input)
        {
            double[,] equivalentDamping = new double[input.NumberOfBoundaryConditions, input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                for (int j = 0; j < input.NumberOfBoundaryConditions; j++)
                {
                    equivalentDamping[i, j] = GetA2(input.TimeStep) * input.Mass[i, j] + GetA3() * input.Damping[i, j];
                }
            }

            return equivalentDamping;
        }

        /// <summary>
        /// This method calculates the equivalent mass to be used in Newmark-Beta method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private double[,] CalculateEquivalentMass(NumericalMethodInput input)
        {
            double[,] equivalentMass = new double[input.NumberOfBoundaryConditions, input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                for (int j = 0; j < input.NumberOfBoundaryConditions; j++)
                {
                    equivalentMass[i, j] = GetA4() * input.Mass[i, j] + GetA5(input.TimeStep) * input.Damping[i, j];
                }
            }

            return equivalentMass;
        }

        #region Integration Constants

        private double GetA0(double timeStep) => 1 / (this.Beta * Math.Pow(timeStep, 2));

        private double GetA1(double timeStep) => this.Gama / (this.Beta * timeStep);

        private double GetA2(double timeStep) => 1 / (this.Beta * timeStep);

        private double GetA3() => this.Gama / this.Beta;

        private double GetA4() => 1 / (2 * this.Beta);

        private double GetA5(double timeStep) => -timeStep * (1 - this.Gama / (2 * this.Beta));

        #endregion
    }
}
