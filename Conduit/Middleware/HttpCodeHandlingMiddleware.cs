using Azure;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Web;

namespace Conduit.Middleware
{
    public class HttpCodeHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpCodeHandlingMiddleware> _logger;

        public HttpCodeHandlingMiddleware(RequestDelegate next, ILogger<HttpCodeHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
                await _next(context);
                HandleHttpExceptionAsync(context);  
        }

        public async Task HandleHttpExceptionAsync(HttpContext context)
        {
                _logger.LogInformation($"code:{context.Response.StatusCode}// time:{DateTime.UtcNow}");
                var error = context.Response.StatusCode switch
                {
                    (int)HttpStatusCode.Unauthorized => new Error("No access", "You arent logged in"),
                    (int)HttpStatusCode.Forbidden => new Error("No access", "You dont have rights to access")
                };    
                await context.Response.WriteAsJsonAsync(error);

        }

        private record Error(string Type, string Reason);

    }

    public static class ErrorHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder app) 
        {
            return app.UseMiddleware<HttpCodeHandlingMiddleware>();
        }
    }
}
