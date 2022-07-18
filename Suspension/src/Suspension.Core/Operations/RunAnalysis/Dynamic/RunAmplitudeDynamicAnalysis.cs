using MudRunner.Commons.Core.Operation;
using MudRunner.Commons.DataContracts.Operation;
using MudRunner.Suspension.Core.ExtensionMethods;
using MudRunner.Suspension.DataContracts.Models.Enums;
using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Dynamic
{
    /// <summary>
    /// It is responsible to run the dynamic analysis to suspension system focusing in the amplitude of the system.
    /// </summary>
    public abstract class RunAmplitudeDynamicAnalysis<TRunAmplitudeDynamicAnalysisRequest, TRunDynamicAnalysisRequest> :
        OperationBase<TRunAmplitudeDynamicAnalysisRequest, OperationResponse<RunAmplitudeDynamicAnalysisResponseData>>,
        IRunAmplitudeDynamicAnalysis<TRunAmplitudeDynamicAnalysisRequest, TRunDynamicAnalysisRequest>
        where TRunAmplitudeDynamicAnalysisRequest : RunGenericDynamicAnalysisRequest
        where TRunDynamicAnalysisRequest : RunGenericDynamicAnalysisRequest
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

        private readonly IRunDynamicAnalysis<TRunDynamicAnalysisRequest> _runDynamicAnalysis;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="runDynamicAnalysis"></param>
        protected RunAmplitudeDynamicAnalysis(IRunDynamicAnalysis<TRunDynamicAnalysisRequest> runDynamicAnalysis)
        {
            this._runDynamicAnalysis = runDynamicAnalysis;
        }

        /// <summary>
        /// Asynchronously, this method run the dynamic analysis to suspension system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected async override Task<OperationResponse<RunAmplitudeDynamicAnalysisResponseData>> ProcessOperationAsync(TRunAmplitudeDynamicAnalysisRequest request)
        {
            OperationResponse<RunAmplitudeDynamicAnalysisResponseData> response = new();

            // Step 1 - Build a list of request for operation RunDynamicAnalysis.
            List<TRunDynamicAnalysisRequest> runDynamicAnalysisRequestList = await this.BuildRunDynamicAnalysisRequestListAsync(request).ConfigureAwait(false);

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

            try
            {
                using (StreamWriter resultStreamWriter = new(resultFullFileName))
                using (StreamWriter deformationStreamWriter = new(deformationFullFileName))
                {
                    // Step 3 - Write the header in the file.
                    resultStreamWriter.WriteLine(this.CreateResultFileHeader());
                    deformationStreamWriter.WriteLine(this.CreateDeformationResultFileHeader());

                    foreach (TRunDynamicAnalysisRequest runDynamicAnalysisRequest in runDynamicAnalysisRequestList)
                    {
                        int requestIndex = runDynamicAnalysisRequestList.IndexOf(runDynamicAnalysisRequest);
                        runDynamicAnalysisRequest.AdditionalFileNameInformation = $"{request.AdditionalFileNameInformation}_Index-{requestIndex}";

                        // Step 4 - Executes the RunDynamicAnalysis operation for a single request.
                        var runDynamicAnalysisResponse = await this._runDynamicAnalysis.ProcessAsync(runDynamicAnalysisRequest).ConfigureAwait(false);

                        // Step 5 - If the RunDynamicAnalysis operation failed, save the erros in the response.
                        // OBS.: The main operation must not end if the RunDynamicAnalysis operation failed, it must tries to proces the 
                        // next RunDynamicAnalysisRequest.
                        if (runDynamicAnalysisResponse.Success == false)
                        {
                            response.AddWarning("MESSAGECODE_NOTIMPLEMENTED", $"Ocurred error while processing the request index '{requestIndex}' for RunDynamicAnalysis operation.");
                            response.AddMessages(runDynamicAnalysisResponse.Messages);
                        }
                        else
                        {
                            // Stepp 6 - Write the request in a file with the same name returned by RunDynamicAnalysis operation.
                            string operationResultFullFileName = runDynamicAnalysisResponse.Data.FullFileNames[0];
                            string fileExtension = Path.GetExtension(operationResultFullFileName);
                            string requestFullFileName = operationResultFullFileName.Replace(fileExtension, ".json");
                            File.WriteAllText(requestFullFileName, JToken.FromObject(runDynamicAnalysisRequest).ToString());

                            // Step 7 - Map to response the full file name of the request and the full file name returned
                            // by RunDynamicAnalysis operation.
                            response.Data.FullFileNames.Add(requestFullFileName);
                            response.Data.FullFileNames.AddRange(runDynamicAnalysisResponse.Data.FullFileNames);

                            // Step 8 - Write the maximum result and maximum deformation result for the current request of
                            // RunDynamicAnalysis operation.
                            resultStreamWriter.WriteLine($"{requestIndex},{runDynamicAnalysisResponse.Data.MaximumResult}");
                            deformationStreamWriter.WriteLine($"{requestIndex},{runDynamicAnalysisResponse.Data.MaximumDeformationResult}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.SetInternalServerError($"Error trying to calculate and write the result in file. {ex}.");
            }
            finally
            {
                // Step 9 - Set the success as true and the HTTP Status Code as:
                //    Created (202) - If the number of full file names in response divided by the number of files per request of 
                //    RunDynamicAnalysis operation plus 1 is equal to the number of requests to process.
                //    Partial Content (206) - Otherwise.
                // OBS.: It has the "divided by" because for each request it must have more than one files: one with the all request
                //       content and the files generated by RunDynamicAnalysis operation.
                if (response.Data.FullFileNames.Count / (this._runDynamicAnalysis.NumberOfFilesPerRequest + 1) == runDynamicAnalysisRequestList.Count)
                {
                    response.SetSuccessCreated();
                }
                else if (response.Data.FullFileNames.Count < runDynamicAnalysisRequestList.Count)
                {
                    response.SetSuccessPartialContent();
                }

                // Step 10 - At the end of the process, map the full name of solution file in the response.
                response.Data.FullFileNames.Add(resultFullFileName);
            }

            return response;
        }

        /// <inheritdoc />
        public abstract Task<List<TRunDynamicAnalysisRequest>> BuildRunDynamicAnalysisRequestListAsync(TRunAmplitudeDynamicAnalysisRequest request);

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
        public abstract string CreateResultFileHeader();

        /// <inheritdoc/>
        public abstract string CreateDeformationResultFileHeader();

        /// <summary>
        /// This method creates the solution file name.
        /// </summary>
        /// <param name="additionalFileNameInformation"></param>
        /// <returns></returns>
        /// TODO: Tentar usar regex para que não precise criar um método só para isso.
        protected abstract string CreateSolutionFileName(string additionalFileNameInformation);

        /// <summary>
        /// Asynchronously, this method validates the <see cref="RunGenericDynamicAnalysisRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override Task<OperationResponse<RunAmplitudeDynamicAnalysisResponseData>> ValidateOperationAsync(TRunAmplitudeDynamicAnalysisRequest request)
        {
            OperationResponse<RunAmplitudeDynamicAnalysisResponseData> response = new();
            response.SetSuccessOk();

            if (request.TimeStep < 0)
                response.SetBadRequestError($"The time step: '{request.TimeStep}' must be greather than zero.");

            if (request.FinalTime < 0)
                response.SetBadRequestError($"The final time: '{request.FinalTime}' must be greather than zero.");

            if (request.TimeStep >= request.FinalTime)
                response.SetBadRequestError($"The time step: '{request.TimeStep}' must be smaller than final time: '{request.FinalTime}'.");

            if (Enum.IsDefined(request.DifferentialEquationMethodEnum) == false)
                response.SetBadRequestError($"Invalid {nameof(request.DifferentialEquationMethodEnum)}: '{request.DifferentialEquationMethodEnum}'.");

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
