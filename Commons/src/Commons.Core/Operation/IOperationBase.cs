using MudRunner.Commons.DataContracts.Operation;

namespace MudRunner.Commons.Core.Operation
{
    /// <summary>
    /// Base for all operations in the application.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IOperationBase<TRequest, TResponse>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponse, new()
    {
        /// <summary>
        /// Asynchronously, validates the operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> ValidateAsync(TRequest request);

        /// <summary>
        /// The main method of all operations.
        /// Asynchronously, orchestrates the operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> ProcessAsync(TRequest request);
    }
}