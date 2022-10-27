using System.Net;

namespace Campaign.Application.Exceptions
{
    public class CampaignNotFoundException : WarningException
    {
        public CampaignNotFoundException(string errorMessage) : base(errorMessage, HttpStatusCode.NotFound)
        {
            ErrorMessage = errorMessage;
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}