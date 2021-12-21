using MudRunner.Suspension.DataContracts.OperationBase;
using System;
using System.Threading.Tasks;

namespace MudRunner.Suspension.Core.Operations.Base
{
    /// <summary>
    /// It represents the base for all operations in the application.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class OperationBase<TRequest, TResponse> : IOperationBase<TRequest, TResponse>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponseBase, new()
    {
        /// <summary>
        /// Asynchronously, this method processes the operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected abstract Task<TResponse> ProcessOperationAsync(TRequest request);

        /// <summary>
        /// Asynchronously, this method validates the request sent to operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual Task<TResponse> ValidateOperationAsync(TRequest request)
        {
            var response = new TResponse();
            response.SetSuccessOk();

            if (request == null)
            {
                response.SetBadRequestError("Request cannot be null");
            }

            return Task.FromResult(response);
        }

        /// <summary>
        /// The main method of all operations.
        /// Asynchronously, this method orchestrates the operations.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TResponse> ProcessAsync(TRequest request)
        {
            var response = new TResponse();

            try
            {
                response = await this.ValidateOperationAsync(request).ConfigureAwait(false);
                if (response.Success == false)
                {
                    response.SetBadRequestError();

                    return response;
                }

                response = await this.ProcessOperationAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                response.SetInternalServerError($"{ex}");
            }

            return response;
        }
    }
}
