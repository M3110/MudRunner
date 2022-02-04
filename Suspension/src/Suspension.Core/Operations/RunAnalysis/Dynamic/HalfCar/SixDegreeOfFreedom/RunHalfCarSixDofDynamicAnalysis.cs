using MudRunner.Commons.Core.ExtensionMethods;
using MudRunner.Commons.Core.Factory.DifferentialEquationMethod;
using MudRunner.Commons.Core.Models;
using MudRunner.Suspension.Core.Mapper;
using MudRunner.Suspension.Core.Models;
using MudRunner.Suspension.Core.Models.NumericalMethod;
using MudRunner.Suspension.Core.Utils;
using MudRunner.Suspension.DataContracts.Models;
using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic.HalfCar.SixDegreeOfFreedom;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Dynamic.HalfCar.SixDegreeOfFreedom
{
    /*
     * Indexes:
     *   0 - car linear displacement/velocity/acceleration
     *   1 - car angular displacement/velocity/acceleration
     *   2 - engine linear displacement/velocity/acceleration
     *   3 - driver linear displacement/velocity/acceleration
     *   4 - rear linear displacement/velocity/acceleration
     *   5 - front linear displacement/velocity/acceleration
     */

    /// <summary>
    /// It is responsible to run the dynamic analysis to suspension system considering half car and six degrees of freedom.
    /// </summary>
    public class RunHalfCarSixDofDynamicAnalysis : RunDynamicAnalysis<RunHalfCarSixDofDynamicAnalysisRequest>, IRunHalfCarSixDofDynamicAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="differentialEquationMethodFactory"></param>
        /// <param name="mappingResolver"></param>
        public RunHalfCarSixDofDynamicAnalysis(
            IDifferentialEquationMethodFactory differentialEquationMethodFactory,
            IMappingResolver mappingResolver)
            : base(differentialEquationMethodFactory, mappingResolver) { }

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
            stiffness[0, 0] = request.RearStiffness + request.FrontStiffness + request.EngineMountStiffness + request.SeatStiffness;
            stiffness[0, 1] = -request.RearStiffness * request.RearDistance + request.FrontStiffness * request.FrontDistance - request.EngineMountStiffness * request.EngineDistance + request.SeatStiffness * request.DriverDistance;
            stiffness[0, 2] = -request.EngineMountStiffness;
            stiffness[0, 3] = -request.SeatStiffness;
            stiffness[0, 4] = -request.RearStiffness;
            stiffness[0, 5] = -request.FrontStiffness;
            // Second row.
            stiffness[1, 0] = -request.RearStiffness * request.RearDistance + request.FrontStiffness * request.FrontDistance - request.EngineMountStiffness * request.EngineDistance + request.SeatStiffness * request.DriverDistance;
            stiffness[1, 1] = request.RearStiffness * Math.Pow(request.RearDistance, 2) + request.FrontStiffness * Math.Pow(request.FrontDistance, 2)
                + request.EngineMountStiffness * Math.Pow(request.EngineDistance, 2) + request.SeatStiffness * Math.Pow(request.DriverDistance, 2);
            stiffness[1, 2] = request.EngineMountStiffness * request.EngineDistance;
            stiffness[1, 3] = -request.SeatStiffness * request.DriverDistance;
            stiffness[1, 4] = request.RearStiffness * request.RearDistance;
            stiffness[1, 5] = -request.FrontStiffness * request.FrontDistance;
            // Third row.
            stiffness[2, 0] = -request.EngineMountStiffness;
            stiffness[2, 1] = request.EngineMountStiffness * request.EngineDistance;
            stiffness[2, 2] = request.EngineMountStiffness;
            stiffness[2, 3] = 0;
            stiffness[2, 4] = 0;
            stiffness[2, 5] = 0;
            // Forth row.
            stiffness[3, 0] = -request.SeatStiffness;
            stiffness[3, 1] = -request.SeatStiffness * request.DriverDistance;
            stiffness[3, 2] = 0;
            stiffness[3, 3] = request.SeatStiffness;
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
            // The engine frequency is in RPM and the speed of the car is in kilometer per hour when recieved in the request
            // and they must be converted to radian per second and meter per second, respectively, because all calculations
            // must be done with the units according to International System of Units.
            double engineFrequency = UnitConverter.FromRpmToRads(request.EngineFrequency);
            double carSpeed = UnitConverter.FromKmhToMs(request.BaseExcitation.CarSpeed);

            double[] appliedForce = new double[this.NumberOfBoundaryConditions];
            appliedForce[0] = -request.CarMass * Constants.GravityAcceleration;
            appliedForce[1] = (request.RearMassDistribution * request.RearDistance - request.FrontMassDistribution * request.FrontDistance) * request.CarMass * Constants.GravityAcceleration;
            appliedForce[2] = -request.EngineMass * Constants.GravityAcceleration - request.EngineForce * Math.Sin(engineFrequency * time);
            appliedForce[3] = -request.DriverMass * Constants.GravityAcceleration;
            appliedForce[4] = -request.RearUnsprungMass * Constants.GravityAcceleration;
            appliedForce[5] = -request.FrontUnsprungMass * Constants.GravityAcceleration;

            double[,] equivalentStiffness = new double[this.NumberOfBoundaryConditions, this.NumberOfBoundaryConditions];
            equivalentStiffness[4, 4] = request.RearTireStiffness;
            equivalentStiffness[5, 5] = request.FrontTireStiffness;

            double[] baseExcitationDisplacement = this.CalculateBaseExcitationDisplacement(request.BaseExcitation, request.RearDistance + request.FrontDistance, carSpeed, time);

            // [Equivalent Force] = [Applied Force] + [Equivalent Stiffness] * [Base Excitation]
            return Task.FromResult(appliedForce.Sum(equivalentStiffness.Multiply(baseExcitationDisplacement)));
        }

        /// <inheritdoc/>
        public override string CreateResultFileHeader()
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
        public override string CreateDeformationResultFileHeader()
        {
            StringBuilder fileHeader = new("Time");

            // Step i - Add the header for deformation.
            fileHeader.Append(",Engine mount deformation,Seat deformation,Rear deformation,Front deformation,Rear tire deformation,Front tire deformation");

            // Step ii - Add the header for deformation velocity.
            fileHeader.Append(",Engine mount deformation velocity,Seat deformation velocity,Rear deformation velocity,Front deformation velocity,Rear tire deformation velocity,Front tire deformation velocity");

            // Step iii - Add the header for deformation acceleration.
            fileHeader.Append(",Engine mount deformation acceleration,Seat deformation acceleration,Rear deformation acceleration,Front deformation acceleration,Rear tire deformation acceleration,Front tire deformation acceleration");

            return fileHeader.ToString();
        }

        /// <inheritdoc/>
        protected override string CreateSolutionFileName(string additionalFileNameInformation)
        {
            StringBuilder fileName = new($"HalfCar_DOF-{this.NumberOfBoundaryConditions}_");

            if (string.IsNullOrWhiteSpace(additionalFileNameInformation) == false)
                fileName.Append($"{additionalFileNameInformation}_");

            fileName.Append($"{DateTime.UtcNow:yyyyMMddHHmmss}.csv");

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

        /// <inheritdoc/>
        public override NumericalMethodResult CalculateDeformationResult(RunHalfCarSixDofDynamicAnalysisRequest request, NumericalMethodResult result, double time)
        {
            // The speed of the car is in kilometers per hour when recieved in the request and it must be converted to meters per second
            // because all calculations must be done with the units according to International System of Units.
            double carSpeed = UnitConverter.FromKmhToMs(request.BaseExcitation.CarSpeed);

            // Calculates the base excitation displacement, velocity and acceleration to be used while calculating the tire deformations.
            double[] baseExcitationDisplacement = this.CalculateBaseExcitationDisplacement(request.BaseExcitation, request.RearDistance + request.FrontDistance, carSpeed, time);
            double[] baseExcitationVelocity = this.CalculateBaseExcitationVelocity(request.BaseExcitation, request.RearDistance + request.FrontDistance, carSpeed, time);
            double[] baseExcitationAcceleration = this.CalculateBaseExcitationAcceleration(request.BaseExcitation, request.RearDistance + request.FrontDistance, carSpeed, time);

            NumericalMethodResult deformationResult = new(this.NumberOfBoundaryConditions);
            
            deformationResult.Time = time;

            deformationResult.Displacement[0] = this.CalculateEngineMountDeformation(result.Displacement, request.EngineDistance);
            deformationResult.Displacement[1] = this.CalculateSeatDeformation(result.Displacement, request.DriverDistance);
            deformationResult.Displacement[2] = this.CalculateRearDeformation(result.Displacement, request.RearDistance);
            deformationResult.Displacement[3] = this.CalculateFrontDeformation(result.Displacement, request.FrontDistance);
            deformationResult.Displacement[4] = this.CalculateTireDeformation(result.Displacement[4], baseExcitationDisplacement[4]);
            deformationResult.Displacement[5] = this.CalculateTireDeformation(result.Displacement[5], baseExcitationDisplacement[5]);

            deformationResult.Velocity[0] = this.CalculateEngineMountDeformation(result.Velocity, request.EngineDistance);
            deformationResult.Velocity[1] = this.CalculateSeatDeformation(result.Velocity, request.DriverDistance);
            deformationResult.Velocity[2] = this.CalculateRearDeformation(result.Velocity, request.RearDistance);
            deformationResult.Velocity[3] = this.CalculateFrontDeformation(result.Velocity, request.FrontDistance);
            deformationResult.Velocity[4] = this.CalculateTireDeformation(result.Velocity[4], baseExcitationVelocity[4]);
            deformationResult.Velocity[5] = this.CalculateTireDeformation(result.Velocity[5], baseExcitationVelocity[5]);

            deformationResult.Acceleration[0] = this.CalculateEngineMountDeformation(result.Acceleration, request.EngineDistance);
            deformationResult.Acceleration[1] = this.CalculateSeatDeformation(result.Acceleration, request.DriverDistance);
            deformationResult.Acceleration[2] = this.CalculateRearDeformation(result.Acceleration, request.RearDistance);
            deformationResult.Acceleration[3] = this.CalculateFrontDeformation(result.Acceleration, request.FrontDistance);
            deformationResult.Acceleration[4] = this.CalculateTireDeformation(result.Acceleration[4], baseExcitationAcceleration[4]);
            deformationResult.Acceleration[5] = this.CalculateTireDeformation(result.Acceleration[5], baseExcitationAcceleration[5]);

            return deformationResult;
        }

        private double[] CalculateBaseExcitationDisplacement(BaseExcitation baseExcitation, double carSize, double carSpeed, double time)
        {
            double[] baseExcitationDisplacement = new double[this.NumberOfBoundaryConditions];
            baseExcitationDisplacement[4] = BaseExcitationUtils.CalculateDisplacement(baseExcitation, time - carSize / carSpeed);
            baseExcitationDisplacement[5] = BaseExcitationUtils.CalculateDisplacement(baseExcitation, time);

            return baseExcitationDisplacement;
        }

        private double[] CalculateBaseExcitationVelocity(BaseExcitation baseExcitation, double carSize, double carSpeed, double time)
        {
            double[] baseExcitationVelocity = new double[this.NumberOfBoundaryConditions];
            baseExcitationVelocity[4] = BaseExcitationUtils.CalculateVelocity(baseExcitation, time - carSize / carSpeed);
            baseExcitationVelocity[5] = BaseExcitationUtils.CalculateVelocity(baseExcitation, time);

            return baseExcitationVelocity;
        }

        private double[] CalculateBaseExcitationAcceleration(BaseExcitation baseExcitation, double carSize, double carSpeed, double time)
        {
            double[] baseExcitationAcceleration = new double[this.NumberOfBoundaryConditions];
            baseExcitationAcceleration[4] = BaseExcitationUtils.CalculateAcceleration(baseExcitation, time - carSize / carSpeed);
            baseExcitationAcceleration[5] = BaseExcitationUtils.CalculateAcceleration(baseExcitation, time);

            return baseExcitationAcceleration;
        }

        /// <summary>
        /// This method calculates the engine mount deformation.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="engineDistance"></param>
        /// <returns></returns>
        private double CalculateEngineMountDeformation(double[] result, double engineDistance) => result[0] - engineDistance * result[1] - result[2];

        /// <summary>
        /// This method calculates the seat deformation.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="driverDistance"></param>
        /// <returns></returns>
        private double CalculateSeatDeformation(double[] result, double driverDistance) => result[0] + driverDistance * result[1] - result[3];

        /// <summary>
        /// This method calculates the rear deformation.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="rearDistance"></param>
        /// <returns></returns>
        private double CalculateRearDeformation(double[] result, double rearDistance) => result[0] - rearDistance * result[1] - result[4];

        /// <summary>
        /// This method calculates the front deformation.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="frontDistance"></param>
        /// <returns></returns>
        private double CalculateFrontDeformation(double[] result, double frontDistance) => result[0] + frontDistance * result[1] - result[5];

        /// <summary>
        /// This method calculates the deformation of tire.
        /// </summary>
        /// <param name="suspensionResult"></param>
        /// <param name="baseExcitation"></param>
        /// <returns></returns>
        private double CalculateTireDeformation(double suspensionResult, double baseExcitation) => suspensionResult - baseExcitation;
    }
}
