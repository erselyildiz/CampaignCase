using System.Net;

namespace Campaign.Api.Models
{
    public class BaseResponseViewModel<T>
    {
        public T Data { get; set; }
    }

    public class BaseResponseViewModel
    {
        public ErrorViewModel Error { get; set; }
    }

    public class ErrorViewModel
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}