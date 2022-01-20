using MudRunner.Commons.Core.Operation;
using MudRunner.Suspension.Core.Models.NumericalMethod.Newmark;
using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic;
using System.Threading.Tasks;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Dynamic
{
    /// <summary>
    /// It is responsible to run the dynamic analysis to suspension system.
    /// </summary>
    public interface IRunDynamicAnalysis : IOperationBase<RunDynamicAnalysisRequest, RunDynamicAnalysisResponse>
    {
        /// <summary>
        /// Asynchronously, this method builds the input for numerical method.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<NewmarkMethodInput> BuildNumericalMethodInputAsync(RunDynamicAnalysisRequest request);

        /// <summary>
        /// Asynchronously, this method calculates the mass matrix.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<double[,]> BuildMassMatrixAsync(RunDynamicAnalysisRequest request);

        /// <summary>
        /// Asynchronously, this method calculates the damping matrix.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<double[,]> BuildDampingMatrixAsync(RunDynamicAnalysisRequest request);

        /// <summary>
        /// Asynchronously, this method calculates the stiffness matrix.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<double[,]> BuildStiffnessMatrixAsync(RunDynamicAnalysisRequest request);

        /// <summary>
        /// Asynchronously, this method calculates the external forcing vector.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<double[]> BuildEquivalentForceVectorAsync(RunDynamicAnalysisRequest request, double time);

        /// <summary>
        /// This method creates the solution file.
        /// </summary>
        /// <param name="additionalFileNameInformation"></param>
        /// <param name="fullFileName">The full name of solution file.</param>
        /// <returns>True, if the file was created. False, otherwise.</returns>
        bool TryCreateSolutionFile(string additionalFileNameInformation, out string fullFileName);

        /// <summary>
        /// This method creates the file header with the results order.
        /// </summary>
        /// <returns></returns>
        string CreateFileHeader();
    }
}