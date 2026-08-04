using ClinicBooking.Domain.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace ClinicBooking.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

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
            catch (ValidationException ex)
            {
                _logger.LogWarning("Validation error: {Errors}", ex.Errors);
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "Validation failed",
                    ex.Errors.Select(e => e.ErrorMessage).ToList());
            }
            catch (DomainException ex)
            {
                _logger.LogWarning("Domain error: {Message}", ex.Message);
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.");
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            HttpStatusCode statusCode,
            string message,
            List<string>? errors = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                status = (int)statusCode,
                message,
                errors = errors ?? new List<string>()
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}