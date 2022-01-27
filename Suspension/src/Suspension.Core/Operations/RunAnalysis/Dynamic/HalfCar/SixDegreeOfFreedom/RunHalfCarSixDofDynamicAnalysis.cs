using MudRunner.Commons.Core.ExtensionMethods;
using MudRunner.Commons.Core.Factory.DifferentialEquationMethod;
using MudRunner.Commons.Core.Models;
using MudRunner.Suspension.Core.Models;
using MudRunner.Suspension.Core.Models.NumericalMethod;
using MudRunner.Suspension.Core.Utils;
using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic.HalfCar.SixDegreeOfFreedom;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Dynamic.HalfCar.SixDegreeOfFreedom
{
    /// <summary>
    /// It is responsible to run the dynamic analysis to suspension system considering half car and six degrees of freedom.
    /// </summary>
    public class RunHalfCarSixDofDynamicAnalysis : RunDynamicAnalysis<RunHalfCarSixDofDynamicAnalysisRequest>, IRunHalfCarSixDofDynamicAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="differentialEquationMethodFactory"></param>
        public RunHalfCarSixDofDynamicAnalysis(IDifferentialEquationMethodFactory differentialEquationMethodFactory) : base(differentialEquationMethodFactory) { }

        /// <inheritdoc/>
        protected override uint NumberOfBoundaryConditions => 6;

        /// <inheritdoc/>
        protected override string SolutionPath => BasePaths.HalfCarSixDofAnalysis;

        /// <inheritdoc/>
        public override Task<double[,]> BuildMassMatrixAsync(RunHalfCarSixDofDynamicAnalysisRequest request)
        {
            double[,] mass = new double[this.NumberOfBoundaryConditions, this.NumberOfBoundaryConditions];
            mass[0, 0] = request.CarMass;
            mass[1, 1] = request.CarMomentOfInertia;
            mass[2, 2] = request.EngineMass;
            mass[3, 3] = request.DriverMass;
            mass[4, 4] = request.RearUnsprungMass;
            mass[5, 5] = request.FrontUnsprungMass;

            return Task.FromResult(mass);
        }

        /// <inheritdoc/>
        public override Task<double[,]> BuildDampingMatrixAsync(RunHalfCarSixDofDynamicAnalysisRequest request)
        {
            double[,] damping = new double[this.NumberOfBoundaryConditions, this.NumberOfBoundaryConditions];
            // First row.
            damping[0, 0] = request.RearDamping + request.FrontDamping;
            damping[0, 1] = -request.RearDamping * request.RearDistance + request.FrontDamping * request.FrontDistance;
            damping[0, 2] = 0;
            damping[0, 3] = 0;
            damping[0, 4] = -request.RearDamping;
            damping[0, 5] = -request.FrontDamping;
            // Second row.
            damping[1, 0] = -request.RearDamping * request.RearDistance + request.FrontDamping * request.FrontDistance;
            damping[1, 1] = request.RearDamping * Math.Pow(request.RearDistance, 2) + request.FrontDamping * Math.Pow(request.FrontDistance, 2);
            damping[1, 2] = 0;
            damping[1, 3] = 0;
            damping[1, 4] = request.RearDamping * request.RearDistance;
            damping[1, 5] = -request.FrontDamping * request.FrontDistance;
            // Third row.
            damping[2, 0] = 0;
            damping[2, 1] = 0;
            damping[2, 2] = 0;
            damping[2, 3] = 0;
            damping[2, 4] = 0;
            damping[2, 5] = 0;
            // Forth row.
            damping[3, 0] = 0;
            damping[3, 1] = 0;
            damping[3, 2] = 0;
            damping[3, 3] = 0;
            damping[3, 4] = 0;
            damping[3, 5] = 0;
            // Fifth row.
            damping[4, 0] = -request.RearDamping;
            damping[4, 1] = request.RearDamping * request.RearDistance;
            damping[4, 2] = 0;
            damping[4, 3] = 0;
            damping[4, 4] = request.RearDamping;
            damping[4, 5] = 0;
            // Sixth row.
            damping[5, 0] = -request.FrontDamping;
            damping[5, 1] = -request.FrontDamping * request.FrontDistance;
            damping[5, 2] = 0;
            damping[5, 3] = 0;
            damping[5, 4] = 0;
            damping[5, 5] = request.FrontDamping;

            return Task.FromResult(damping);
        }

        /// <inheritdoc/>
        public override Task<double[,]> BuildStiffnessMatrixAsync(RunHalfCarSixDofDynamicAnalysisRequest request)
        {
            double[,] stiffness = new double[this.NumberOfBoundaryConditions, this.NumberOfBoundaryConditions];
            stiffness[0, 0] = request.RearStiffness + request.FrontStiffness - request.EngineMountStiffness - request.SeatStiffness;
            stiffness[0, 1] = -request.RearStiffness * request.RearDistance + request.FrontStiffness * request.FrontDistance + request.EngineMountStiffness * request.EngineDistance - request.SeatStiffness * request.DriverDistance;
            stiffness[0, 2] = request.EngineMountStiffness;
            stiffness[0, 3] = request.SeatStiffness;
            stiffness[0, 4] = -request.RearStiffness;
            stiffness[0, 5] = -request.FrontStiffness;
            // Second row.
            stiffness[1, 0] = -request.RearStiffness * request.RearDistance + request.FrontStiffness * request.FrontDistance + request.EngineMountStiffness * request.EngineDistance - request.SeatStiffness * request.DriverDistance;
            stiffness[1, 1] = request.RearStiffness * Math.Pow(request.RearDistance, 2) + request.FrontStiffness * Math.Pow(request.FrontDistance, 2)
                - request.EngineMountStiffness * Math.Pow(request.EngineDistance, 2) - request.SeatStiffness * Math.Pow(request.DriverDistance, 2);
            stiffness[1, 2] = -request.EngineMountStiffness * request.EngineDistance;
            stiffness[1, 3] = request.SeatStiffness * request.DriverDistance;
            stiffness[1, 4] = request.RearStiffness * request.RearDistance;
            stiffness[1, 5] = -request.FrontStiffness * request.FrontDistance;
            // Third row.
            stiffness[2, 0] = request.EngineMountStiffness;
            stiffness[2, 1] = -request.EngineMountStiffness * request.EngineDistance;
            stiffness[2, 2] = -request.EngineMountStiffness;
            stiffness[2, 3] = 0;
            stiffness[2, 4] = 0;
            stiffness[2, 5] = 0;
            // Forth row.
            stiffness[3, 0] = request.SeatStiffness;
            stiffness[3, 1] = request.SeatStiffness * request.DriverDistance;
            stiffness[3, 2] = 0;
            stiffness[3, 3] = -request.SeatStiffness;
            stiffness[3, 4] = 0;
            stiffness[3, 5] = 0;
            // Fifth row.
            stiffness[4, 0] = -request.RearStiffness;
            stiffness[4, 1] = request.RearStiffness * request.RearDistance;
            stiffness[4, 2] = 0;
            stiffness[4, 3] = 0;
            stiffness[4, 4] = request.RearStiffness + request.RearTireStiffness;
            stiffness[4, 5] = 0;
            // Sixth row.
            stiffness[5, 0] = -request.FrontStiffness;
            stiffness[5, 1] = -request.FrontStiffness * request.FrontDistance;
            stiffness[5, 2] = 0;
            stiffness[5, 3] = 0;
            stiffness[5, 4] = 0;
            stiffness[5, 5] = request.FrontStiffness + request.FrontTireStiffness;

            return Task.FromResult(stiffness);
        }

        /// <inheritdoc/>
        public override Task<double[]> BuildEquivalentForceVectorAsync(RunHalfCarSixDofDynamicAnalysisRequest request, double time)
        {
            double[] appliedForce = new double[this.NumberOfBoundaryConditions];
            appliedForce[0] = -request.CarMass * Constants.GravityAcceleration;
            appliedForce[1] = (request.RearMassDistribution * request.RearDistance - request.FrontMassDistribution * request.FrontDistance) * request.CarMass * Constants.GravityAcceleration;
            appliedForce[2] = -request.EngineMass * Constants.GravityAcceleration - request.EngineForce * Math.Sin(request.EngineFrequency * time);
            appliedForce[3] = -request.DriverMass * Constants.GravityAcceleration;
            appliedForce[4] = -request.RearUnsprungMass * Constants.GravityAcceleration;
            appliedForce[5] = -request.FrontUnsprungMass * Constants.GravityAcceleration;

            double[,] equivalentStiffness = new double[this.NumberOfBoundaryConditions, this.NumberOfBoundaryConditions];
            equivalentStiffness[4, 4] = request.RearTireStiffness;
            equivalentStiffness[5, 5] = request.FrontTireStiffness;

            // The speed of the car is in kilometers per hour when recieved in the request and it must be converted to meters per second
            // because all calculations must be done with the units according to International System of Units.
            double carSpeed = UnitConverter.FromKmHToMS(request.BaseExcitation.CarSpeed);

            double rearBaseExcitation = BaseExcitationUtils.Calculate(request.BaseExcitation, time - (request.RearDistance + request.FrontDistance) / carSpeed);
            double frontBaseExcitation = BaseExcitationUtils.Calculate(request.BaseExcitation, time);

            double[] baseExcitation = new double[this.NumberOfBoundaryConditions];
            baseExcitation[4] = rearBaseExcitation;
            baseExcitation[5] = frontBaseExcitation;

            // [Equivalent Force] = [Applied Force] + [Equivalent Stiffness] * [Base Excitation]
            var a = appliedForce.Sum(equivalentStiffness.Multiply(baseExcitation));
            return Task.FromResult(a);
        }

        /// <inheritdoc/>
        public override string CreateFileHeader()
        {
            StringBuilder fileHeader = new("Time");

            // Step i - Add the header for displacement.
            fileHeader.Append(",Car linear displacement,Car angular displacement,Engine linear displacement,Driver linear displacement,Rear linear displacement,Front linear displacement");

            // Step ii - Add the header for velocity.
            fileHeader.Append(",Car linear velocity,Car angular velocity,Engine linear velocity,Driver linear velocity,Rear linear velocity,Front linear velocity");

            // Step iii - Add the header for acceleration.
            fileHeader.Append(",Car linear acceleration,Car angular acceleration,Engine linear acceleration,Driver linear acceleration,Rear linear acceleration,Front linear acceleration");

            // Step iv - Add the header for equivalente force.
            fileHeader.Append(",Car equivalente force,Car equivalente torque,Engine equivalente force,Driver equivalente force,Rear equivalente force,Front equivalente force");

            return fileHeader.ToString();
        }

        /// <inheritdoc/>
        protected override string CreateSolutionFileName(string additionalFileNameInformation)
        {
            StringBuilder fileName = new($"HalfCar_DOF-{this.NumberOfBoundaryConditions}_");

            if (string.IsNullOrWhiteSpace(additionalFileNameInformation) == false)
                fileName.Append($"{additionalFileNameInformation}_");

            fileName.Append($"{DateTime.UtcNow:yyyy-MM-dd HH-mm-ss}.csv");

            return fileName.ToString();
        }

        /// <inheritdoc/>
        public override NumericalMethodResult BuildLargeDisplacementResult(NumericalMethodResult result)
        {
            NumericalMethodResult largeDisplacementResult = new()
            {
                Time = result.Time,
                Displacement = new double[this.NumberOfBoundaryConditions],
                Velocity = new double[this.NumberOfBoundaryConditions],
                Acceleration = new double[this.NumberOfBoundaryConditions],
                EquivalentForce = new double[this.NumberOfBoundaryConditions]
            };

            for (int i = 0; i < this.NumberOfBoundaryConditions; i++)
            {
                // 1 is the index of angular displacement.
                if (i == 1)
                {
                    largeDisplacementResult.Displacement[i] = Math.Asin(result.Displacement[i]);
                    largeDisplacementResult.Velocity[i] = result.Velocity[i] / Math.Cos(largeDisplacementResult.Displacement[i]);
                    largeDisplacementResult.Acceleration[i] = result.Acceleration[i] * Math.Cos(largeDisplacementResult.Displacement[i]);
                }
                else
                {
                    largeDisplacementResult.Displacement[i] = result.Displacement[i];
                    largeDisplacementResult.Velocity[i] = result.Velocity[i];
                    largeDisplacementResult.Acceleration[i] = result.Acceleration[i];
                }

                largeDisplacementResult.EquivalentForce[i] = result.EquivalentForce[i];
            }

            return largeDisplacementResult;
        }
    }
}
