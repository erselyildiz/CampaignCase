using System;
using System.Net;

namespace Campaign.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ValidationException(string errorMessage)
        {
            ErrorMessage = errorMessage;
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}