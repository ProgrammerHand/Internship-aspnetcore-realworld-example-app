using System.Net;

namespace Conduit.Infrastructure.Middleware
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
                var error = context.Response.StatusCode switch
                {
                    (int)HttpStatusCode.Unauthorized => new Error("No access", "You arent logged in"),
                    (int)HttpStatusCode.Forbidden => new Error("No access", "You dont have rights to access")
                };    
                await context.Response.WriteAsJsonAsync(error);

        }

        private record Error(string Type, string Reason);

    }

    public static class HttpCodeHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseHttpCodeHandlingMiddleware(this IApplicationBuilder app) 
        {
            return app.UseMiddleware<HttpCodeHandlingMiddleware>();
        }
    }
}
