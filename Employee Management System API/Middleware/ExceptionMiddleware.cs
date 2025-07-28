using System.Net;
using Employee_Management_System_API.DTOs.Global_Error;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Data.SqlClient;
namespace Employee_Management_System_API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ExceptionMiddleware(RequestDelegate next,
                                   ILogger<ExceptionMiddleware> logger,
                                   IWebHostEnvironment webHostEnvironment)
        {
            _next = next;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SqlException ex)
            {                
                _logger.LogError(ex, "An SQL Server error occurred.");
                var errorMessage = "The database is currently unavailable!";
                await HandleExceptionAsync(context,
                                           HttpStatusCode.ServiceUnavailable,
                                           errorMessage,
                                           ex);                                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Unhandled exception occurred");
                var errorMessage = "An internal server error occurred.";                
                await HandleExceptionAsync(context,
                                          HttpStatusCode.InternalServerError,
                                          errorMessage,
                                          ex);                
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message, Exception ex)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorDetails
            {
                Status = (int)statusCode,
                Error = statusCode.ToString(),
                Message = _webHostEnvironment.IsDevelopment() ? ex.Message : null, // details shown in the development only
                Path = context.Request.Path,
                TraceId = context.TraceIdentifier
            };

            await context.Response.WriteAsJsonAsync(errorResponse);            
        }
    }
}
