using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace UnitConversationAPI.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An exception occurred.");

            var (statusCode, title, detail) = exception switch
            {
                ArgumentException =>
                    (StatusCodes.Status400BadRequest,
                     "Invalid conversion request",
                     exception.Message),

                NotSupportedException =>
                    (StatusCodes.Status400BadRequest,
                     "Unsupported conversion",
                     exception.Message),

                _ =>
                    (StatusCodes.Status500InternalServerError,
                     "Internal server error",
                     "Something went wrong. Please try again later.")
            };

            httpContext.Response.StatusCode = statusCode;

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request.Path
            };

            await httpContext.Response.WriteAsJsonAsync(
                problem,
                cancellationToken);

            return true;
        }
    }
}
