using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

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
            try
            {
                await _next(context);
            }
            catch(ArgumentException e)
            {
                await HandleErorrAsync(e, context);
            }
        }

        public async Task HandleErorrAsync(Exception e, HttpContext context)
        {
            _logger.LogInformation($"code:{context.Response.StatusCode}// message {e.Message} // page: {context.Request.Path}");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(e.Message);
        }
    }
    public static class ErrorHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
