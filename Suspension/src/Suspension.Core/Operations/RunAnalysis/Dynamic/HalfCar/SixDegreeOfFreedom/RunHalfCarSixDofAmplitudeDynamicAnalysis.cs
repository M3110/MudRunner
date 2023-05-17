using MudRunner.Suspension.Core.Models;
using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic.HalfCar.SixDegreeOfFreedom;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Dynamic.HalfCar.SixDegreeOfFreedom
{
    /// <summary>
    /// It is responsible to run the dynamic analysis to suspension system focusing in the amplitude of the system 
    /// considering half car and six degrees of freedom.
    /// </summary>
    public class RunHalfCarSixDofAmplitudeDynamicAnalysis :
        RunAmplitudeDynamicAnalysis<
            RunHalfCarSixDofAmplitudeDynamicAnalysisRequest,
            RunHalfCarSixDofDynamicAnalysisRequest>,
        IRunHalfCarSixDofAmplitudeDynamicAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="runHalfCarSixDofDynamicAnalysis"></param>
        public RunHalfCarSixDofAmplitudeDynamicAnalysis(IRunHalfCarSixDofDynamicAnalysis runHalfCarSixDofDynamicAnalysis)
            : base(runHalfCarSixDofDynamicAnalysis) { }

        /// <inheritdoc/>
        protected override uint NumberOfBoundaryConditions => 6;

        /// <inheritdoc/>
        protected override string SolutionPath => SuspensionBasePath.HalfCarSixDofAmplitudeAnalysis;

        /// <inheritdoc/>
        protected override string AnalysisType => DinamicAnalysisTypeName.HalfCar;

        /// <inheritdoc/>
        public override Task<List<RunHalfCarSixDofDynamicAnalysisRequest>> BuildRunDynamicAnalysisRequestListAsync(RunHalfCarSixDofAmplitudeDynamicAnalysisRequest request)
        {
            List<RunHalfCarSixDofDynamicAnalysisRequest> list = new();

            foreach (double frontMassDistribution in request.FrontMassDistribution)
            {
                foreach (double rearMassDistribution in request.RearMassDistribution)
                {
                    foreach (double carMass in request.CarMass)
                    {
                        foreach (double carMomentOfInertia in request.CarMomentOfInertia)
                        {
                            foreach (double engineMass in request.EngineMass)
                            {
                                foreach (double engineMountStiffness in request.EngineMountStiffness)
                                {
                                    foreach (double engineDistance in request.EngineDistance)
                                    {
                                        foreach (double engineForce in request.EngineForce)
                                        {
                                            foreach (double engineFrequency in request.EngineFrequency)
                                            {
                                                foreach (double rearDamping in request.RearDamping)
                                                {
                                                    foreach (double rearStiffness in request.RearStiffness)
                                                    {
                                                        foreach (double rearTireStiffness in request.RearTireStiffness)
                                                        {
                                                            foreach (double rearDistance in request.RearDistance)
                                                            {
                                                                foreach (double frontDamping in request.FrontDamping)
                                                                {
                                                                    foreach (double frontStiffness in request.FrontStiffness)
                                                                    {
                                                                        foreach (double frontTireStiffness in request.FrontTireStiffness)
                                                                        {
                                                                            foreach (double frontDistance in request.FrontDistance)
                                                                            {
                                                                                foreach (double rearUnsprungMass in request.RearUnsprungMass)
                                                                                {
                                                                                    foreach (double frontUnsprungMass in request.FrontUnsprungMass)
                                                                                    {
                                                                                        foreach (double driverMass in request.DriverMass)
                                                                                        {
                                                                                            foreach (double seatStiffness in request.SeatStiffness)
                                                                                            {
                                                                                                foreach (double driverDistance in request.DriverDistance)
                                                                                                {
                                                                                                    list.Add(new RunHalfCarSixDofDynamicAnalysisRequest()
                                                                                                    {
                                                                                                        ConsiderLargeDisplacements = request.ConsiderLargeDisplacements,
                                                                                                        TimeStep = request.TimeStep,
                                                                                                        FinalTime = request.FinalTime,
                                                                                                        DifferentialEquationMethodEnum = request.DifferentialEquationMethodEnum,
                                                                                                        BaseExcitation = request.BaseExcitation,
                                                                                                        FrontMassDistribution = frontMassDistribution,
                                                                                                        RearMassDistribution = rearMassDistribution,
                                                                                                        CarMass = carMass,
                                                                                                        CarMomentOfInertia = carMomentOfInertia,
                                                                                                        EngineMass = engineMass,
                                                                                                        EngineMountStiffness = engineMountStiffness,
                                                                                                        EngineDistance = engineDistance,
                                                                                                        EngineForce = engineForce,
                                                                                                        EngineFrequency = engineFrequency,
                                                                                                        RearDamping = rearDamping,
                                                                                                        RearStiffness = rearStiffness,
                                                                                                        RearTireStiffness = rearTireStiffness,
                                                                                                        RearDistance = rearDistance,
                                                                                                        FrontDamping = frontDamping,
                                                                                                        FrontStiffness = frontStiffness,
                                                                                                        FrontTireStiffness = frontTireStiffness,
                                                                                                        FrontDistance = frontDistance,
                                                                                                        RearUnsprungMass = rearUnsprungMass,
                                                                                                        FrontUnsprungMass = frontUnsprungMass,
                                                                                                        DriverDistance = driverDistance,
                                                                                                        SeatStiffness = seatStiffness,
                                                                                                        DriverMass = driverMass
                                                                                                    });
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return Task.FromResult(list);
        }

        /// <inheritdoc/>
        public override string CreateResultFileHeader()
        {
            StringBuilder fileHeader = new("Request Index");

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
            StringBuilder fileHeader = new("Request Index");

            // Step i - Add the header for deformation.
            fileHeader.Append(",Engine mount deformation,Seat deformation,Rear deformation,Front deformation,Rear tire deformation,Front tire deformation");

            // Step ii - Add the header for deformation velocity.
            fileHeader.Append(",Engine mount deformation velocity,Seat deformation velocity,Rear deformation velocity,Front deformation velocity,Rear tire deformation velocity,Front tire deformation velocity");

            // Step iii - Add the header for deformation acceleration.
            fileHeader.Append(",Engine mount deformation acceleration,Seat deformation acceleration,Rear deformation acceleration,Front deformation acceleration,Rear tire deformation acceleration,Front tire deformation acceleration");

            return fileHeader.ToString();
        }
    }
}
