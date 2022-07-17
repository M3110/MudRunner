using MudRunner.Commons.DataContracts.Operation;
using System.Globalization;

namespace MudRunner.Commons.Core.Operation
{
    /// <summary>
    /// Base for all operations.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    // TODO: Adicionar log.
    public abstract class OperationBase<TRequest, TResponse> : IOperationBase<TRequest, TResponse>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponse, new()
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        protected OperationBase()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Asynchronously, validates the request sent to operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected abstract Task<TResponse> ValidateOperationAsync(TRequest request);

        /// <summary>
        /// Asynchronously, processes the operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected abstract Task<TResponse> ProcessOperationAsync(TRequest request);

        /// <inheritdoc/>
        public async Task<TResponse> ValidateAsync(TRequest request)
        {
            var response = new TResponse();
            response.SetSuccessOk();

            if (request == null)
            {
                response.SetBadRequestError("Request cannot be null");
                return response;
            }

            response = await this.ValidateOperationAsync(request).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<TResponse> ProcessAsync(TRequest request)
        {
            TResponse response = new();

            try
            {
                TResponse validationResponse = await ValidateAsync(request).ConfigureAwait(false);
                if (!validationResponse.Success)
                {
                    response.AddMessages(validationResponse);
                    return response;
                }

                response = await ProcessOperationAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                response.SetInternalServerError($"{ex}");
            }

            return response;
        }
    }
}
