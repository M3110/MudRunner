using MudRunner.Commons.Core.Operation;
using MudRunner.Suspension.Core.Models.NumericalMethod;
using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Dynamic
{
    /// <summary>
    /// It is responsible to run the dynamic analysis to suspension system focusing in the amplitude of the system.
    /// </summary>
    public interface IRunAmplitudeDynamicAnalysis<TRunAmplitudeDynamicAnalysisRequest, TRunDynamicAnalysisRequest> : 
        IOperationBase<TRunAmplitudeDynamicAnalysisRequest, RunAmplitudeDynamicAnalysisResponse>
        where TRunAmplitudeDynamicAnalysisRequest : RunGenericDynamicAnalysisRequest
        where TRunDynamicAnalysisRequest : RunGenericDynamicAnalysisRequest
    {
        /// <summary>
        /// Asynchronously, this method builds a list of request for operation <see cref="RunDynamicAnalysis{TRunDynamicAnalysisRequest}"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        // TODO: usar Asyncenumerable com SemaphoreSlim e permitir que sejam feitas no máximo 4 threads em paralelo.
        Task<List<TRunDynamicAnalysisRequest>> BuildRunDynamicAnalysisRequestListAsync(TRunAmplitudeDynamicAnalysisRequest request);

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
        string CreateResultFileHeader();

        /// <summary>
        /// This method creates the file header with the deformation results order.
        /// </summary>
        /// <returns></returns>
        string CreateDeformationResultFileHeader();
    }
}