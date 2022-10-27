using System;
using System.Net;

namespace Campaign.Application.Exceptions
{
    public class WarningException : Exception
    {
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public WarningException(string errorMessage, HttpStatusCode statusCode)
        {
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }
    }
}