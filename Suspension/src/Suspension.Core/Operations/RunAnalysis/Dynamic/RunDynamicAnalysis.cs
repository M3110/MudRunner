using MudRunner.Commons.Core.Factory.DifferentialEquationMethod;
using MudRunner.Commons.Core.Models;
using MudRunner.Commons.Core.Operation;
using MudRunner.Suspension.Core.ExtensionMethods;
using MudRunner.Suspension.Core.Models.NumericalMethod;
using MudRunner.Suspension.Core.NumericalMethods.DifferentialEquation;
using MudRunner.Suspension.DataContracts.Models.Enums;
using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Dynamic
{
    /// <summary>
    /// It is responsible to run the dynamic analysis to suspension system.
    /// </summary>
    public abstract class RunDynamicAnalysis<TRequest> : OperationBase<TRequest, RunDynamicAnalysisResponse>, IRunDynamicAnalysis<TRequest>
        where TRequest : RunDynamicAnalysisRequest
    {
        /// <summary>
        /// The number of boundary conditions for dynamic analysis.
        /// Dimensionless.
        /// </summary>
        protected abstract uint NumberOfBoundaryConditions { get; }

        /// <summary>
        /// The path for solution file.
        /// </summary>
        protected abstract string SolutionPath { get; }

        private IDifferentialEquationMethod _differentialEquationMethod;

        private readonly IDifferentialEquationMethodFactory _differentialEquationMethodFactory;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="differentialEquationMethodFactory"></param>
        protected RunDynamicAnalysis(IDifferentialEquationMethodFactory differentialEquationMethodFactory)// : base()
        {
            this._differentialEquationMethodFactory = differentialEquationMethodFactory;
        }

        /// <summary>
        /// Asynchronously, this method run the dynamic analysis to suspension system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected async override Task<RunDynamicAnalysisResponse> ProcessOperationAsync(TRequest request)
        {
            RunDynamicAnalysisResponse response = new();
            response.SetSuccessOk();

            // Step 1 - Build the input for numerical method.
            NumericalMethodInput input = await this.BuildNumericalMethodInputAsync(request).ConfigureAwait(false);

            // Step 2 - Creates the solutions file and the folder if they do not exist.
            if (this.TryCreateSolutionFile(request.AdditionalFileNameInformation, out string solutionFullFileName) == false)
            {
                response.SetConflictError($"The file '{solutionFullFileName}' already exist.");
                return response;
            }

            try
            {
                using (StreamWriter streamWriter = new(solutionFullFileName))
                {
                    // Step 3 - Write the header in the file.
                    streamWriter.WriteLine(this.CreateFileHeader());

                    // Step 4 - Calculates the result for initial time.
                    NumericalMethodResult previousResult = this._differentialEquationMethod.CalculateInitialResult(input);

                    double time = Constants.InitialTime + request.TimeStep;
                    while (time <= request.FinalTime)
                    {
                        // Step 5 - Calculate the results and write it in the file.
                        NumericalMethodResult result = await this._differentialEquationMethod.CalculateResultAsync(input, previousResult, time).ConfigureAwait(false);
                        if (request.ConsiderLargeDisplacements == false)
                        {
                            streamWriter.WriteLine($"{result}");
                        }
                        else
                        {
                            // OBS.: 
                            //   The variable 'largeDisplacementResult' must only be used 
                            NumericalMethodResult largeDisplacementResult = this.BuildLargeDisplacementResult(result);
                            streamWriter.WriteLine($"{largeDisplacementResult}");
                        }

                        // Step 6 - Save the current result in the variable 'previousResult' to be used at next step
                        // and itereta the time.
                        previousResult = result;
                        time += input.TimeStep;

                        // Step 7 - Calculates the equivalent force to be used at next step.
                        input.EquivalentForce = await this.BuildEquivalentForceVectorAsync(request, time).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                response.SetInternalServerError($"Error trying to calculate and write the result in file. {ex}.");
            }
            finally
            {
                // Step 8 - At the end of the process, map the full name of solution file in the response
                // and set the success as true and HTTP Status Code as 200 (OK).
                response.Data.FullFileName = solutionFullFileName;
            }

            return response;
        }

        /// <inheritdoc />
        public async Task<NumericalMethodInput> BuildNumericalMethodInputAsync(TRequest request)
        {
            // Step i - Create the mass, the stiffness and the damping matrixes, and the equivalent force vector.
            double[,] mass = default;
            double[,] stiffness = default;
            double[,] damping = default;
            double[] equivalentForce = default;

            // Step ii - Asynchronously, build the mass, the stiffness and the damping matrixes, and the equivalent force vector.
            List<Task> tasks = new()
            {
                Task.Run(async () => mass = await this.BuildMassMatrixAsync(request).ConfigureAwait(false)),
                Task.Run(async () => stiffness = await this.BuildStiffnessMatrixAsync(request).ConfigureAwait(false)),
                Task.Run(async () => damping = await this.BuildDampingMatrixAsync(request).ConfigureAwait(false)),
                Task.Run(async () => equivalentForce = await this.BuildEquivalentForceVectorAsync(request, Constants.InitialTime).ConfigureAwait(false)),
            };

            // Step iii - Wait all tasks to be executed.
            await Task.WhenAll(tasks).ConfigureAwait(false);

            // Step iv - Map the matrixes and vector to the numerical method input and return it.
            return new()
            {
                Mass = mass,
                Stiffness = stiffness,
                Damping = damping,
                EquivalentForce = equivalentForce,
                NumberOfBoundaryConditions = this.NumberOfBoundaryConditions,
                TimeStep = request.TimeStep
            };
        }

        /// <inheritdoc/>
        public abstract Task<double[,]> BuildMassMatrixAsync(TRequest request);

        /// <inheritdoc/>
        public abstract Task<double[,]> BuildDampingMatrixAsync(TRequest request);

        /// <inheritdoc/>
        public abstract Task<double[,]> BuildStiffnessMatrixAsync(TRequest request);

        /// <inheritdoc/>
        public abstract Task<double[]> BuildEquivalentForceVectorAsync(TRequest request, double time);

        /// <inheritdoc/>
        public bool TryCreateSolutionFile(string additionalFileNameInformation, out string fullFileName)
        {
            FileInfo fileInfo = new(Path.Combine(
                this.SolutionPath,
                this.CreateSolutionFileName(additionalFileNameInformation)));

            if (fileInfo.Exists)
            {
                fullFileName = fileInfo.FullName;
                return false;
            }

            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();

            fullFileName = fileInfo.FullName;
            return true;
        }

        /// <inheritdoc/>
        public abstract string CreateFileHeader();

        /// <summary>
        /// This method creates the solution file name.
        /// </summary>
        /// <param name="additionalFileNameInformation"></param>
        /// <returns></returns>
        /// TODO: Tentar usar regex para que não precise criar um método só para isso.
        protected abstract string CreateSolutionFileName(string additionalFileNameInformation);

        /// <inheritdoc/>
        public abstract NumericalMethodResult BuildLargeDisplacementResult(NumericalMethodResult result);

        /// <summary>
        /// Asynchronously, this method validates the <see cref="RunDynamicAnalysisRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override Task<RunDynamicAnalysisResponse> ValidateOperationAsync(TRequest request)
        {
            RunDynamicAnalysisResponse response = new();
            response.SetSuccessOk();

            if (request.TimeStep <= 0)
                response.SetBadRequestError($"The time step: '{request.TimeStep}' must be greather zero.");

            if (request.FinalTime <= 0)
                response.SetBadRequestError($"The final time: '{request.FinalTime}' must be greather zero.");

            if (request.TimeStep >= request.FinalTime)
                response.SetBadRequestError($"The time step: '{request.TimeStep}' must be smaller than final time: '{request.FinalTime}'.");

            if (Enum.IsDefined(request.DifferentialEquationMethodEnum) == false)
                response.SetBadRequestError($"Invalid {nameof(request.DifferentialEquationMethodEnum)}: '{request.DifferentialEquationMethodEnum}'.");

            this._differentialEquationMethod = this._differentialEquationMethodFactory.Get(request.DifferentialEquationMethodEnum);
            if (this._differentialEquationMethod == null)
                response.SetBadRequestError($"The differential method '{request.DifferentialEquationMethodEnum}' was not registered in '{nameof(IDifferentialEquationMethodFactory)}'.");

            if (request.BaseExcitation != null)
            {
                if (request.BaseExcitation.Constants.IsNullOrEmpty())
                    response.SetBadRequestError($"'{nameof(request.BaseExcitation.Constants)}' cannot be null or empty.");

                if (Enum.IsDefined(request.BaseExcitation.CurveType) == false)
                    response.SetBadRequestError($"Invalid curve type: '{request.BaseExcitation.CurveType}'.");

                if (request.BaseExcitation.CurveType == CurveType.Cosine)
                {
                    if (request.BaseExcitation.ObstacleWidth <= 0)
                        response.SetBadRequestError($"'{nameof(request.BaseExcitation.ObstacleWidth)}' must be greather than zero.");

                    if (request.BaseExcitation.CarSpeed == 0)
                        response.SetBadRequestError($"'{nameof(request.BaseExcitation.CarSpeed)}' cannot be zero.");
                }
            }

            return Task.FromResult(response);
        }
    }
}
