using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace CodingChallenge.Api
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log issues and handle exception response

            var error = new ApiError
            {
                Id = Guid.NewGuid().ToString(),
            };
            var errorMessage = string.Empty;
            if (exception.GetType() == typeof(ValidationException))
            {
                error.Status = (short)HttpStatusCode.BadRequest;
                var validationException = (ValidationException)exception;
                error.Code = "Validation";
                error.Title = validationException.Message;

            }

            else
            {
                error.Status = (short)HttpStatusCode.InternalServerError;
                errorMessage = GetInnermostExceptionMessage(exception);
                error.Code = "InternalError";
                error.Title = errorMessage;
            }

            _logger.LogError(exception, "Error!!! " + errorMessage + " -- {ErrorId}.", error.Id);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.Status;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }

        private string GetInnermostExceptionMessage(Exception exception)
        {
            if (exception.InnerException != null)
                return GetInnermostExceptionMessage(exception.InnerException);

            return exception.Message;
        }
    }
}
