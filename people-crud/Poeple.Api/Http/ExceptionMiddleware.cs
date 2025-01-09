using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace People.Api.Http
{
    public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment environment) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var traceId = Guid.NewGuid();
                logger.LogError($"Error occure while processing the request, TraceId : ${traceId}, Message : ${ex.Message}, StackTrace: ${ex.StackTrace}");

                var httpError = GetHttpErrorFromException(ex, traceId);

                context.Response.StatusCode = httpError.StatusCode;

                await context.Response.WriteAsJsonAsync(httpError);
            }
        }

        private HttpResponseErrors GetHttpErrorFromException(Exception exception, Guid traceId)
        {
            var exceptionType = exception.InnerException?.GetType();
            if (exceptionType == typeof(Application.Exceptions.ValidationException) || exceptionType == typeof(ValidationException))
            {
                return new ((int)StatusCodes.Status400BadRequest, "Bad Request", $"The resource already exists");
            }
            return new ((int)StatusCodes.Status500InternalServerError, "Internal Server Error", environment.IsDevelopment() ? exception.Message : $"Internal server error occured, traceId : {traceId}");
        }

        private class HttpResponseErrors
        {
            public HttpResponseErrors(int statusCode, string title, object? value = null) => (StatusCode, Title, Value) = (statusCode, title, value);

            public int StatusCode { get; }
            public string? Title { get; }
            public object? Value { get; }
        }
    }
}
