using Microsoft.AspNetCore.Mvc;
using MudRunner.Commons.DataContracts.Operation;
using System.Net;

namespace MudRunner.Suspension.Application.Extensions
{
    /// <summary>
    /// It is responsible to build the HTTP response.
    /// </summary>
    public static class HttpResponseBuilder
    {
        /// <summary>
        /// This method builds the HTTP response .
        /// </summary>
        /// <typeparam name="TResponseData"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static JsonResult BuildHttpResponse<TResponseData>(this OperationResponse<TResponseData> response)
            where TResponseData : OperationResponseData, new()
        {
            return new JsonResult(response)
            {
                StatusCode = (int)response.HttpStatusCode
            };
        }
    }
}
