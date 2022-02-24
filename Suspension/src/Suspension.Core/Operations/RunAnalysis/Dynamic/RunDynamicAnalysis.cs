using MudRunner.Commons.Core.Factory.DifferentialEquationMethod;
using MudRunner.Commons.Core.Models;
using MudRunner.Commons.Core.Operation;
using MudRunner.Suspension.Core.ExtensionMethods;
using MudRunner.Suspension.Core.Mapper;
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
        where TRequest : RunGenericDynamicAnalysisRequest
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

        /// <inheritdoc/>
        public uint NumberOfFilesPerRequest => 2;

        private IDifferentialEquationMethod _differentialEquationMethod;

        private readonly IDifferentialEquationMethodFactory _differentialEquationMethodFactory;
        private readonly IMappingResolver _mappingResolver;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="differentialEquationMethodFactory"></param>
        /// <param name="mappingResolver"></param>
        protected RunDynamicAnalysis(IDifferentialEquationMethodFactory differentialEquationMethodFactory, IMappingResolver mappingResolver)
        {
            this._differentialEquationMethodFactory = differentialEquationMethodFactory;
            this._mappingResolver = mappingResolver;
        }

        /// <summary>
        /// Asynchronously, this method run the dynamic analysis to suspension system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected async override Task<RunDynamicAnalysisResponse> ProcessOperationAsync(TRequest request)
        {
            RunDynamicAnalysisResponse response = new();

            // Step 1 - Build the input for numerical method.
            NumericalMethodInput input = await this.BuildNumericalMethodInputAsync(request).ConfigureAwait(false);

            // Step 2 - Create the file that will contain the results from numerical model and its folder if they do not exist.
            // The results written in that file represents the displacement, velocity and acceleration of each boundary condition
            // in relation to the origin of the system.
            if (this.TryCreateSolutionFile(request.AdditionalFileNameInformation, out string resultFullFileName) == false)
            {
                response.SetConflictError($"The file '{resultFullFileName}' already exist.");
                return response;
            }

            // Step 3 - Create the file that will contain the real results of each boundary condition and at and its
            // folder if they do not exist.
            // The results written in that file represents the real deformation, deformation velocity and deformation acceleration
            // in relation to the origin of the system.
            // OBS.:
            //   The deformation velocity is the deformation derivative on time and represents how the deformation varies on time.
            //   The deformation acceleration is the second deformation derivative on time and represents how the deformation
            //   velocity varies on time.
            if (this.TryCreateSolutionFile($"{request.AdditionalFileNameInformation}_Deformation", out string deformationFullFileName) == false)
            {
                response.SetConflictError($"The file '{resultFullFileName}' already exist.");
                return response;
            }

            // Step 4 - Instantiate the variable 'maximumResult' that will contains the maximum result for analysis and the variable
            // 'maximumDeformationResult' that will contain the maximum deformation for analysis.
            NumericalMethodResult maximumResult = new(this.NumberOfBoundaryConditions);
            NumericalMethodResult maximumDeformationResult = new(this.NumberOfBoundaryConditions);

            try
            {
                using (StreamWriter resultStreamWriter = new(resultFullFileName))
                using (StreamWriter deformationStreamWriter = new(deformationFullFileName))
                {
                    // Step 5 - Write the header in the file.
                    resultStreamWriter.WriteLine(this.CreateResultFileHeader());
                    deformationStreamWriter.WriteLine(this.CreateDeformationResultFileHeader());

                    // Step 6 - Calculate the result for initial time.
                    NumericalMethodResult previousResult = this._differentialEquationMethod.CalculateInitialResult(input);
                    maximumResult = previousResult;
                    maximumDeformationResult = previousResult;

                    double time = Constants.InitialTime + request.TimeStep;
                    while (time <= request.FinalTime)
                    {
                        // Step 7 - Calculate the results and write it in the file.
                        NumericalMethodResult result = await this._differentialEquationMethod.CalculateResultAsync(input, previousResult, time).ConfigureAwait(false);
                        if (request.ConsiderLargeDisplacements == false)
                        {
                            resultStreamWriter.WriteLine($"{result}");

                            NumericalMethodResult deformationResult = this.CalculateDeformationResult(request, result, time);
                            deformationStreamWriter.WriteLine($"{deformationResult}");

                            maximumResult = this.GetMaximumResult(maximumResult, result);
                            maximumDeformationResult = this.GetMaximumResult(maximumDeformationResult, deformationResult);
                        }
                        else
                        {
                            NumericalMethodResult largeDisplacementResult = this.BuildLargeDisplacementResult(result);
                            resultStreamWriter.WriteLine($"{largeDisplacementResult}");

                            NumericalMethodResult deformationResult = this.CalculateDeformationResult(request, largeDisplacementResult, time);
                            deformationStreamWriter.WriteLine($"{deformationResult}");

                            maximumResult = this.GetMaximumResult(maximumResult, largeDisplacementResult);
                            maximumDeformationResult = this.GetMaximumResult(maximumDeformationResult, deformationResult);
                        }

                        // Step 8 - Save the current result in the variable 'previousResult' to be used at next step
                        // and iterate the time.
                        previousResult = result;
                        time += input.TimeStep;

                        // Step 9 - Calculates the equivalent force to be used at next step.
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
                // Step 9 - At the end of the process, it maps the full name of solution file and the maximum result to response
                // and set the success as true and HTTP Status Code as 201 (Created).
                response.Data.FullFileNames.Add(resultFullFileName);
                response.Data.FullFileNames.Add(deformationFullFileName);
                response.Data.MaximumResult = this._mappingResolver.MapFrom(maximumResult);
                response.Data.MaximumDeformationResult = this._mappingResolver.MapFrom(maximumDeformationResult);
                response.SetSuccessCreated();
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
            // TODO: Criar interface para checar se arquivo existe e criar arquivo
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
        public abstract string CreateResultFileHeader();

        /// <inheritdoc/>
        public abstract string CreateDeformationResultFileHeader();

        /// <inheritdoc/>
        public abstract NumericalMethodResult BuildLargeDisplacementResult(NumericalMethodResult result);

        /// <inheritdoc/>
        public abstract NumericalMethodResult CalculateDeformationResult(TRequest request, NumericalMethodResult result, double time);

        /// <summary>
        /// This method creates the solution file name.
        /// </summary>
        /// <param name="additionalFileNameInformation"></param>
        /// <returns></returns>
        /// TODO: Tentar usar regex para que não precise criar um método só para isso.
        protected abstract string CreateSolutionFileName(string additionalFileNameInformation);

        /// <summary>
        /// This method returns the maximum results between two <see cref="NumericalMethodResult"/>.
        /// </summary>
        /// <param name="result1"></param>
        /// <param name="result2"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected NumericalMethodResult GetMaximumResult(NumericalMethodResult result1, NumericalMethodResult result2)
        {
            NumericalMethodResult maximumResult = new(this.NumberOfBoundaryConditions);

            for (int i = 0; i < this.NumberOfBoundaryConditions; i++)
            {
                maximumResult.Displacement[i] = this.GetMaximumAbsolutValue(result1.Displacement[i], result2.Displacement[i]);
                maximumResult.Velocity[i] = this.GetMaximumAbsolutValue(result1.Velocity[i], result2.Velocity[i]);
                maximumResult.Acceleration[i] = this.GetMaximumAbsolutValue(result1.Acceleration[i], result2.Acceleration[i]);
                maximumResult.EquivalentForce[i] = this.GetMaximumAbsolutValue(result1.EquivalentForce[i], result2.EquivalentForce[i]);
            }

            return maximumResult;
        }

        /// <summary>
        /// This method gets the maximum absulet value between two values.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        protected double GetMaximumAbsolutValue(double value1, double value2)
        {
            if (Math.Abs(value1) > Math.Abs(value2))
                return value1;
            else
                return value2;
        }

        /// <summary>
        /// Asynchronously, this method validates the <see cref="RunGenericDynamicAnalysisRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override Task<RunDynamicAnalysisResponse> ValidateOperationAsync(TRequest request)
        {
            RunDynamicAnalysisResponse response = new();
            response.SetSuccessOk();

            if (request.TimeStep < 0)
                response.SetBadRequestError($"The time step: '{request.TimeStep}' must be greather than zero.");

            if (request.FinalTime < 0)
                response.SetBadRequestError($"The final time: '{request.FinalTime}' must be greather than zero.");

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
