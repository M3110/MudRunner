using System.Net;

namespace MudRunner.Commons.DataContracts.Operation
{
    /// <summary>
    /// It represents the response content for all operations.
    /// </summary>
    public class OperationResponseBase
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public OperationResponseBase()
        {
            this.Reports = new List<string>();
        }

        /// <summary>
        /// The success status of operation.
        /// </summary>
        public bool Success { get; protected set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; protected set; }

        /// <summary>
        /// The list of report.
        /// </summary>
        public List<string> Reports { get; protected set; }

        /// <summary>
        /// This method adds report to the report list.
        /// </summary>
        /// <param name="report"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="success"></param>
        public void AddReport(string report, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, bool success = false)
        {
            if (string.IsNullOrWhiteSpace(report))
                throw new ArgumentNullException(nameof(report), $"The '{nameof(report)}' cannot be null or white space.");

            this.Reports.Add(report);
            this.HttpStatusCode = httpStatusCode;
            this.Success = success;
        }

        /// <summary>
        /// This method adds the operation reports to the report list.
        /// </summary>
        /// <param name="response"></param>
        public void AddReports(OperationResponseBase response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response), "Response cannot be null.");

            if (response.Reports == null)
                throw new ArgumentNullException(nameof(response.Reports), $"The '{nameof(response.Reports)}' cannot be null in the response.");

            if (response.Reports.Count <= 0)
                throw new ArgumentOutOfRangeException(nameof(response.Reports), $"It must contains at least one report in '{nameof(response.Reports)}'.");

            this.Reports.AddRange(response.Reports);
            this.HttpStatusCode = response.HttpStatusCode;
            this.Success = response.Success;
        }

        /// <summary>
        /// This method adds reports to the report list.
        /// </summary>
        /// <param name="reports"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="success"></param>
        public void AddReports(List<string> reports, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, bool success = false)
        {
            if (reports == null)
                throw new ArgumentNullException(nameof(reports), $"The '{nameof(reports)}' cannot be null or white space.");

            if (reports.Count <= 0)
                throw new ArgumentOutOfRangeException(nameof(reports), $"It must contains at least one report in '{nameof(reports)}'.");

            this.Reports.AddRange(reports);
            this.HttpStatusCode = httpStatusCode;
            this.Success = success;
        }

        /// <summary>
        /// This method sets Success to true and the HttpStatusCode to 200 (OK).
        /// </summary>
        public void SetSuccessOk() => this.SetSuccess(HttpStatusCode.OK);

        /// <summary>
        /// This method sets Success to true and the HttpStatusCode to 201 (Created).
        /// </summary>
        public void SetSuccessCreated() => this.SetSuccess(HttpStatusCode.Created);

        /// <summary>
        /// This method sets Success to true and the HttpStatusCode to 202 (Accepted).
        /// </summary>
        public void SetSuccessAccepted() => this.SetSuccess(HttpStatusCode.Accepted);

        /// <summary>
        /// This method sets Success to true and the HttpStatusCode to 206 (PartialContent).
        /// </summary>
        public void SetSuccessPartialContent() => this.SetSuccess(HttpStatusCode.PartialContent);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 400 (BadRequest).
        /// </summary>
        /// <param name="report"></param>
        public void SetBadRequestError(string report = null) => this.SetError(HttpStatusCode.BadRequest, report);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 401 (Unauthorized).
        /// </summary>
        /// <param name="report"></param>
        public void SetUnauthorizedError(string report = null) => this.SetError(HttpStatusCode.Unauthorized, report);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 404 (NotFound).
        /// </summary>
        /// <param name="report"></param>
        public void SetNotFoundError(string report = null) => this.SetError(HttpStatusCode.NotFound, report);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 409 (Conflict).
        /// </summary>
        /// <param name="report"></param>
        public void SetConflictError(string report = null) => this.SetError(HttpStatusCode.Conflict, report);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 417 (ExpectationFailed).
        /// </summary>
        /// <param name="report"></param>
        public void SetExpectationFailedError(string report = null) => this.SetError(HttpStatusCode.ExpectationFailed, report);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 422 (UnprocessableEntity).
        /// </summary>
        /// <param name="report"></param>
        public void SetUnprocessableEntityError(string report = null) => this.SetError(HttpStatusCode.UnprocessableEntity, report);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 423 (Locked).
        /// </summary>
        /// <param name="report"></param>
        public void SetLockedError(string report = null) => this.SetError(HttpStatusCode.Locked, report);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 429 (TooManyRequests).
        /// </summary>
        /// <param name="report"></param>
        public void SetTooManyRequestsError(string report = null) => this.SetError(HttpStatusCode.TooManyRequests, report);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 500 (InternalServerError).
        /// </summary>
        /// <param name="report"></param>
        public void SetInternalServerError(string report = null) => this.SetError(HttpStatusCode.InternalServerError, report);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 501 (NotImplemented).
        /// </summary>
        /// <param name="report"></param>
        public void SetNotImplementedError(string report = null) => this.SetError(HttpStatusCode.NotImplemented, report);

        /// <summary>
        /// This method sets Sucess to true.
        /// </summary>
        /// <param name="httpStatusCode"></param>
        protected void SetSuccess(HttpStatusCode httpStatusCode)
        {
            this.HttpStatusCode = httpStatusCode;
            this.Success = true;
        }

        /// <summary>
        /// This method sets Success to false.
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="report"></param>
        protected void SetError(HttpStatusCode httpStatusCode, string report = null)
        {
            if (report != null)
                this.Reports.Add(report);

            this.HttpStatusCode = httpStatusCode;
            this.Success = false;
        }

        /// <summary>
        /// This method indicates if the HTTP Status Code is success or not.
        /// HTTP Status Codes:
        ///     1xx - Information
        ///     2xx - Success
        ///     3xx - Redirection
        ///     4xx - Client Error
        ///     5xx - Server Error
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <returns></returns>
        protected bool IsSuccessHttpStatusCode(HttpStatusCode httpStatusCode)
        {
            return ((int)httpStatusCode).ToString().StartsWith("2");
        }
    }

    /// <summary>
    /// It represents the response content for all operations.
    /// </summary>
    /// <typeparam name="TResponseData"></typeparam>
    public class OperationResponseBase<TResponseData> : OperationResponseBase
        where TResponseData : OperationResponseData, new()
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public OperationResponseBase()
        {
            this.Reports = new List<string>();
            this.Data = new TResponseData();
        }

        /// <summary>
        /// It represents the 'data' content of all operation response.
        /// </summary>
        public TResponseData Data { get; set; }
    }
}
