using MudRunner.Commons.DataContracts.Operation;

namespace MudRunner.Commons.Core.Operation
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
        public virtual async Task<TResponse> ValidateAsync(TRequest request)
        {
            var response = new TResponse();
            response.SetSuccessOk();

            if (request == null)
            {
                response.SetBadRequestError("Request cannot be null");
                return response;
            }

            return response;
        }

        /// <summary>
        /// The main method of all operations.
        /// Asynchronously, this method orchestrates the operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TResponse> ProcessAsync(TRequest request)
        {
            TResponse response = new();

            try
            {
                TResponse validationResponse = await ValidateAsync(request).ConfigureAwait(false);
                if (validationResponse.Success == false)
                {
                    response.AddErrors(validationResponse.Errors);

                    return validationResponse;
                }

                validationResponse = await ProcessOperationAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                response.SetInternalServerError($"{ex}");
            }

            return response;
        }
    }
}
