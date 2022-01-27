﻿using MudRunner.Commons.Core.Operation;
using MudRunner.Suspension.Core.Models.NumericalMethod;
using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic;
using System.Threading.Tasks;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Dynamic
{
    /// <summary>
    /// It is responsible to run the dynamic analysis to suspension system.
    /// </summary>
    public interface IRunDynamicAnalysis<TRequest> : IOperationBase<TRequest, RunDynamicAnalysisResponse>
        where TRequest : RunDynamicAnalysisRequest
    {
        /// <summary>
        /// Asynchronously, this method builds the input for numerical method.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<NumericalMethodInput> BuildNumericalMethodInputAsync(TRequest request);

        /// <summary>
        /// Asynchronously, this method calculates the mass matrix.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<double[,]> BuildMassMatrixAsync(TRequest request);

        /// <summary>
        /// Asynchronously, this method calculates the damping matrix.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<double[,]> BuildDampingMatrixAsync(TRequest request);

        /// <summary>
        /// Asynchronously, this method calculates the stiffness matrix.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<double[,]> BuildStiffnessMatrixAsync(TRequest request);

        /// <summary>
        /// Asynchronously, this method calculates the external forcing vector.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<double[]> BuildEquivalentForceVectorAsync(TRequest request, double time);

        /// <summary>
        /// This method creates the solution file.
        /// </summary>
        /// <param name="additionalFileNameInformation"></param>
        /// <param name="fullFileName">The full name of solution file.</param>
        /// <returns>True, if the file was created. False, otherwise.</returns>
        bool TryCreateSolutionFile(string additionalFileNameInformation, out string fullFileName);

        /// <summary>
        /// This method creates the file header with the results order.
        /// To implement this method is necessary to know what is written in the file.
        /// Currently, it is necessary to know what is returned in the <see cref="NumericalMethodResult"/>.
        /// </summary>
        /// <returns></returns>
        string CreateFileHeader();

        /// <summary>
        /// This method builds the result for large displacements. It is necessary because when considering large displacements,
        /// the values at <see cref="NumericalMethodResult"/> do not represent the displacement, the velocity or the acceleration, 
        /// and some steps must be done to calculate the real value of those.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        NumericalMethodResult BuildLargeDisplacementResult(NumericalMethodResult result);
    }
}