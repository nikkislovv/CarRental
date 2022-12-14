
using SendGrid.Helpers.Errors.Model;
using Server.Exeptions;
using System.Text.Json;

namespace Server.Extensions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, DateTime.Now);

                await HandleExceptionAsync(context, e);
            }
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var response = new
            {
                status = statusCode,
                message = exception.Message,
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private int GetStatusCode(Exception exception) =>
            exception switch
            {
                ArgumentNullException => StatusCodes.Status400BadRequest,
                BadRequestException => StatusCodes.Status400BadRequest,
                Exeptions.NotFoundException => StatusCodes.Status404NotFound,
                FluentValidation.ValidationException => StatusCodes.Status422UnprocessableEntity,
                OperationCanceledException => 499,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}
