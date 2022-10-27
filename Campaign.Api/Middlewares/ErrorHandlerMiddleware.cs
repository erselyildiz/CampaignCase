using Campaign.Api.Models;
using Campaign.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Campaign.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                BaseResponseViewModel baseResponseViewModel = new()
                {
                    Error = error switch
                    {
                        WarningException exception => new ErrorViewModel
                        {
                            StatusCode = exception.StatusCode,
                            Message = exception.ErrorMessage
                        },
                        ValidationException exception => new ErrorViewModel
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = exception.ErrorMessage
                        },
                        _ => new ErrorViewModel
                        {
                            Message = "An unexpected error was encountered..",
                            StatusCode = HttpStatusCode.InternalServerError
                        },
                    }
                };
                response.StatusCode = (int)baseResponseViewModel.Error.StatusCode;

                await response.WriteAsJsonAsync(baseResponseViewModel);
            }
        }
    }
}