using System.Net;

namespace Campaign.Application.Exceptions
{
    public class NotTypeOfObjectIdException : WarningException
    {
        public NotTypeOfObjectIdException(string errorMessage) : base(errorMessage, HttpStatusCode.NotFound)
        {
            ErrorMessage = errorMessage;
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}