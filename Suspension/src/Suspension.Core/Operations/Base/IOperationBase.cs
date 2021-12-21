using MudRunner.Suspension.DataContracts.OperationBase;
using System.Threading.Tasks;

namespace MudRunner.Suspension.Core.Operations.Base
{
    /// <summary>
    /// It represents the base for all operations in the application.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IOperationBase<TRequest, TResponse>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponseBase, new()
    {
        /// <summary>
        /// The main method of all operations.
        /// Asynchronously, this method orchestrates the operations.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> ProcessAsync(TRequest request);
    }
}