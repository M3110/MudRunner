using MudRunner.Suspension.Core.Models;
using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic.HalfCar.SixDegreeOfFreedom;
using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic.QuarterCar.OneDegreeOfFreedom;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Dynamic.QuarterCar.OneDegreeOfFreedom
{
    /// <summary>
    /// It is responsible to run the dynamic analysis to suspension system focusing in the amplitude of the system 
    /// considering quarter car and one degree of freedom.
    /// </summary>
    public class RunQuarterCarOneDofAmplitudeDynamicAnalysis :
        RunAmplitudeDynamicAnalysis<
            RunQuarterCarOneDofAmplitudeDynamicAnalysisRequest,
            RunQuarterCarOneDofDynamicAnalysisRequest>,
        IRunQuarterCarOneDofAmplitudeDynamicAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="runQuarterCarOneDofDynamicAnalysis"></param>
        public RunQuarterCarOneDofAmplitudeDynamicAnalysis(IRunQuarterCarOneDofDynamicAnalysis runQuarterCarOneDofDynamicAnalysis)
            : base(runQuarterCarOneDofDynamicAnalysis) { }

        /// <inheritdoc/>
        protected override uint NumberOfBoundaryConditions => 1;

        /// <inheritdoc/>
        protected override string SolutionPath => SuspensionBasePath.QuarterCarOneDofAmplitudeAnalysis;

        /// <inheritdoc/>
        protected override string AnalysisType => DinamicAnalysisTypeName.QuarterCar;

        /// <inheritdoc/>
        public override Task<List<RunQuarterCarOneDofDynamicAnalysisRequest>> BuildRunDynamicAnalysisRequestListAsync(RunQuarterCarOneDofAmplitudeDynamicAnalysisRequest request)
        {
            List<RunQuarterCarOneDofDynamicAnalysisRequest> list = new();

            foreach (double mass in request.Mass)
            {
                foreach (double damping in request.Damping)
                {
                    foreach (double stiffness in request.Stiffness)
                    {
                        foreach (double force in request.Force)
                        {
                            foreach (double frequency in request.Frequency)
                            {
                                list.Add(new RunQuarterCarOneDofDynamicAnalysisRequest
                                {
                                    ConsiderLargeDisplacements = request.ConsiderLargeDisplacements,
                                    TimeStep = request.TimeStep,
                                    FinalTime = request.FinalTime,
                                    BaseExcitation = request.BaseExcitation,
                                    DifferentialEquationMethodEnum = request.DifferentialEquationMethodEnum,
                                    AdditionalFileNameInformation = request.AdditionalFileNameInformation,
                                    Mass = mass,
                                    Damping = damping,
                                    Stiffness = stiffness,
                                    Force = force,
                                    Frequency = frequency,
                                });
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
            fileHeader.Append(",Linear displacement");

            // Step ii - Add the header for velocity.
            fileHeader.Append(",Linear velocity");

            // Step iii - Add the header for acceleration.
            fileHeader.Append(",Linear acceleration");

            // Step iv - Add the header for equivalente force.
            fileHeader.Append(",Equivalente force");

            return fileHeader.ToString();
        }

        /// <inheritdoc/>
        public override string CreateDeformationResultFileHeader()
        {
            StringBuilder fileHeader = new("Request Index");

            // Step i - Add the header for deformation.
            fileHeader.Append(",Deformation");

            // Step ii - Add the header for deformation velocity.
            fileHeader.Append(",Deformation velocity");

            // Step iii - Add the header for deformation acceleration.
            fileHeader.Append(",Deformation acceleration");

            return fileHeader.ToString();
        }
    }
}
