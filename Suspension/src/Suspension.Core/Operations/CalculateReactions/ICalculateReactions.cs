using Suspension.Core.Models.SuspensionComponents;
using Suspension.Core.Operations.Base;
using Suspension.DataContracts.CalculateReactions;
using Suspension.DataContracts.Models;

namespace Suspension.Core.Operations.CalculateReactions
{
    /// <summary>
    /// It is responsible to calculate the reactions to suspension system.
    /// </summary>
    public interface ICalculateReactions : IOperationBase<CalculateReactionsRequest, CalculateReactionsResponse> 
    {
        /// <summary>
        /// This method builds the reactions vector.
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        double[] BuildEffortsVector(Vector3D force);

        /// <summary>
        /// This method builds the matrix with normalized force directions and displacements.
        /// </summary>
        /// <param name="suspensionSystem"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        double[,] BuildDisplacementMatrix(SuspensionSystem suspensionSystem, Point3D origin);

        /// <summary>
        /// This method maps the analysis result to response data.
        /// </summary>
        /// <param name="suspensionSystem"></param>
        /// <param name="result"></param>
        /// <param name="shouldRoundResults"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        CalculateReactionsResponseData MapToResponseData(SuspensionSystem suspensionSystem, double[] result, bool shouldRoundResults, int? decimals);
    }
}
