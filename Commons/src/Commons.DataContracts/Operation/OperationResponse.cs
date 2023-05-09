using System.Net;

namespace MudRunner.Commons.DataContracts.Operation
{
    /// <summary>
    /// Response content for all operations.
    /// </summary>
    public class OperationResponse
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public OperationResponse()
        {
            this.Messages = new List<OperationResponseMessage>();
        }

        /// <summary>
        /// The time spent at the operation.
        /// </summary>
        public TimeSpan TimeSpent { get; set; }

        /// <summary>
        /// The success status of operation.
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; private set; }

        /// <summary>
        /// The list of messages.
        /// </summary>
        public List<OperationResponseMessage> Messages { get; private set; }

        /// <summary>
        /// Adds message to the message list.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="success"></param>
        public void AddMessage(OperationResponseMessage message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, bool success = false)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message), $"The '{nameof(message)}' cannot be null.");

            this.Messages.Add(message);
            this.HttpStatusCode = httpStatusCode;
            this.Success = success;
        }

        /// <summary>
        /// Adds message to the message list.
        /// </summary>
        /// <param name="messageContent"></param>
        /// <param name="messageCode"></param>
        public void AddInfo(string messageCode, string messageContent)
        {
            if (messageCode == null)
                throw new ArgumentNullException(nameof(messageCode), $"The '{nameof(messageCode)}' cannot be null.");

            if (messageContent == null)
                throw new ArgumentNullException(nameof(messageContent), $"The '{nameof(messageContent)}' cannot be null.");

            this.Messages.Add(OperationResponseMessage.Create(OperationResponseMessageType.Info, messageCode, messageContent));
        }

        /// <summary>
        /// Adds message to the message list.
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="messageContent"></param>
        public void AddWarning(string messageCode, string messageContent)
        {
            if (messageCode == null)
                throw new ArgumentNullException(nameof(messageCode), $"The '{nameof(messageCode)}' cannot be null.");

            if (messageContent == null)
                throw new ArgumentNullException(nameof(messageContent), $"The '{nameof(messageContent)}' cannot be null.");

            this.Messages.Add(OperationResponseMessage.Create(OperationResponseMessageType.Warning, messageCode, messageContent));
        }

        /// <summary>
        /// Adds the operation messages to the message list.
        /// </summary>
        /// <param name="response"></param>
        public void AddMessages(OperationResponse response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response), "Response cannot be null.");

            if (response.Messages == null)
                throw new ArgumentNullException(nameof(response), $"The '{nameof(response.Messages)}' cannot be null in the response.");

            if (response.Messages.Count <= 0)
                throw new ArgumentOutOfRangeException(nameof(response), $"It must contains at least one message in '{nameof(response.Messages)}'.");

            this.Messages.AddRange(response.Messages);
            this.HttpStatusCode = response.HttpStatusCode;
            this.Success = response.Success;
        }

        /// <summary>
        /// Adds messages to the message list.
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="success"></param>
        public void AddMessages(List<OperationResponseMessage> messages, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, bool success = false)
        {
            if (messages == null)
                throw new ArgumentNullException(nameof(messages), $"The '{nameof(messages)}' cannot be null or white space.");

            if (messages.Count <= 0)
                throw new ArgumentOutOfRangeException(nameof(messages), $"It must contains at least one message in '{nameof(messages)}'.");

            this.Messages.AddRange(messages);
            this.HttpStatusCode = httpStatusCode;
            this.Success = success;
        }

        /// <summary>
        /// Sets Success to true and the HttpStatusCode to 200 (OK).
        /// </summary>
        public void SetSuccessOk() => this.SetSuccess(HttpStatusCode.OK);

        /// <summary>
        /// Sets Success to true and the HttpStatusCode to 201 (Created).
        /// </summary>
        public void SetSuccessCreated() => this.SetSuccess(HttpStatusCode.Created);

        /// <summary>
        /// Sets Success to true and the HttpStatusCode to 202 (Accepted).
        /// </summary>
        public void SetSuccessAccepted() => this.SetSuccess(HttpStatusCode.Accepted);

        /// <summary>
        /// Sets Success to true and the HttpStatusCode to 206 (PartialContent).
        /// </summary>
        public void SetSuccessPartialContent() => this.SetSuccess(HttpStatusCode.PartialContent);

        /// <summary>
        /// Sets Success to false and the HttpStatusCode to 400 (BadRequest).
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="messageContent"></param>
        public void SetBadRequestError(string messageCode = null, string messageContent = null) => this.SetError(HttpStatusCode.BadRequest, messageCode, messageContent);

        /// <summary>
        /// Sets Success to false and the HttpStatusCode to 401 (Unauthorized).
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="messageContent"></param>
        public void SetUnauthorizedError(string messageCode = null, string messageContent = null) => this.SetError(HttpStatusCode.Unauthorized, messageCode, messageContent);

        /// <summary>
        /// Sets Success to false and the HttpStatusCode to 404 (NotFound).
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="messageContent"></param>
        public void SetNotFoundError(string messageCode = null, string messageContent = null) => this.SetError(HttpStatusCode.NotFound, messageCode, messageContent);

        /// <summary>
        /// Sets Success to false and the HttpStatusCode to 409 (Conflict).
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="messageContent"></param>
        public void SetConflictError(string messageCode = null, string messageContent = null) => this.SetError(HttpStatusCode.Conflict, messageCode, messageContent);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 413 (RequestEntityTooLarge).
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="messageContent"></param>
        public void SetRequestEntityTooLargeError(string messageCode = null, string messageContent = null) => SetError(HttpStatusCode.RequestEntityTooLarge, messageCode, messageContent);

        /// <summary>
        /// Sets Success to false and the HttpStatusCode to 417 (ExpectationFailed).
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="messageContent"></param>
        public void SetExpectationFailedError(string messageCode = null, string messageContent = null) => this.SetError(HttpStatusCode.ExpectationFailed, messageCode, messageContent);

        /// <summary>
        /// Sets Success to false and the HttpStatusCode to 422 (UnprocessableEntity).
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="messageContent"></param>
        public void SetUnprocessableEntityError(string messageCode = null, string messageContent = null) => this.SetError(HttpStatusCode.UnprocessableEntity, messageCode, messageContent);

        /// <summary>
        /// Sets Success to false and the HttpStatusCode to 423 (Locked).
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="messageContent"></param>
        public void SetLockedError(string messageCode = null, string messageContent = null) => this.SetError(HttpStatusCode.Locked, messageCode, messageContent);

        /// <summary>
        /// Sets Success to false and the HttpStatusCode to 429 (TooManyRequests).
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="messageContent"></param>
        public void SetTooManyRequestsError(string messageCode = null, string messageContent = null) => this.SetError(HttpStatusCode.TooManyRequests, messageCode, messageContent);

        /// <summary>
        /// Sets Success to false and the HttpStatusCode to 500 (InternalServerError).
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="messageContent"></param>
        public void SetInternalServerError(string messageCode = null, string messageContent = null) => this.SetError(HttpStatusCode.InternalServerError, messageCode, messageContent);

        /// <summary>
        /// Sets Success to false and the HttpStatusCode to 501 (NotImplemented).
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="messageContent"></param>
        public void SetNotImplementedError(string messageCode = null, string messageContent = null) => this.SetError(HttpStatusCode.NotImplemented, messageCode, messageContent);

        /// <summary>
        /// Sets Sucess to true.
        /// </summary>
        /// <param name="httpStatusCode"></param>
        public void SetSuccess(HttpStatusCode httpStatusCode)
        {
            this.HttpStatusCode = httpStatusCode;
            this.Success = true;
        }

        /// <summary>
        /// Sets Success to false.
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="messageCode"></param>
        /// <param name="messageContent"></param>
        public void SetError(HttpStatusCode httpStatusCode, string messageCode = null, string messageContent = null)
        {
            this.Messages.Add(OperationResponseMessage.Create(OperationResponseMessageType.Error, messageCode, messageContent));
            this.HttpStatusCode = httpStatusCode;
            this.Success = false;
        }

        /// <summary>
        /// Indicates if the HTTP Status Code is success or not.
        /// HTTP Status Codes:
        ///     1xx - Information
        ///     2xx - Success
        ///     3xx - Redirection
        ///     4xx - Client Error
        ///     5xx - Server Error
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <returns></returns>
        public bool IsSuccessHttpStatusCode(HttpStatusCode httpStatusCode)
        {
            return ((int)httpStatusCode).ToString().StartsWith("2");
        }
    }

    /// <summary>
    /// Response content for all operations.
    /// </summary>
    /// <typeparam name="TResponseData"></typeparam>
    public sealed class OperationResponse<TResponseData> : OperationResponse
        where TResponseData : class, new()
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public OperationResponse() : base()
        {
            this.Data = new TResponseData();
        }

        /// <summary>
        /// 'Data' content of all operation response.
        /// </summary>
        public TResponseData Data { get; set; }
    }
}
