using System;
using System.Net;
using System.Text.Json.Serialization;
using Thunder.Platform.Core.Exceptions;

namespace Thunder.Platform.AspNetCore.ExceptionHandling
{
    [Serializable]
    public class ErrorResponse
    {
        public ErrorResponse(ErrorInfo error, HttpStatusCode statusCode)
        {
            Error = error;
            StatusCode = (int)statusCode;
        }

        public ErrorResponse(string message)
        {
            Error = new ErrorInfo
            {
                Message = message
            };
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        public ErrorInfo Error { get; }

        [JsonIgnore]
        public int StatusCode { get; }
    }
}
