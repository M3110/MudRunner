using MudRunner.Commons.DataContracts.Operation;

namespace MudRunner.Commons.Core.Operation
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
        /// Asynchronously, this method validates the operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> ValidateAsync(TRequest request);

        /// <summary>
        /// The main method of all operations.
        /// Asynchronously, this method orchestrates the operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> ProcessAsync(TRequest request);
    }
}