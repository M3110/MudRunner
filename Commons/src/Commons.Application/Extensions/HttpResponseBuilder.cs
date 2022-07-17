using Microsoft.AspNetCore.Mvc;
using MudRunner.Commons.DataContracts.Operation;

namespace MudRunner.Suspension.Application.Extensions
{
    /// <summary>
    /// Builds the HTTP response.
    /// </summary>
    public static class HttpResponseBuilder
    {
        /// <summary>
        /// Builds the HTTP response.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static JsonResult BuildHttpResponse(this OperationResponse response)
        {
            return new JsonResult(response)
            {
                StatusCode = (int)response.HttpStatusCode
            };
        }
    }
}
