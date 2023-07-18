using Azure;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Web;

namespace Conduit.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
                await _next(context);
                HandleExceptionAsync(context);  
        }

        public async Task HandleExceptionAsync(HttpContext context)
        {
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized) 
            {
                _logger.LogInformation($"code:{context.Response.StatusCode}// time:{DateTime.UtcNow}");
                //ProblemDetails problemDetails = new()
                //{
                //    Type = "Server error",
                //    Title = "No acces",
                //    Detail = "You arent logged in"
                //};
                var error = new Error("No acces", "You arent logged in");
                await context.Response.WriteAsJsonAsync(error);
            }

        }

        private record Error(string Code, string Reason);

    }

    public static class ErrorHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder app) 
        {
            return app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
