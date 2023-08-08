using Microsoft.Extensions.Primitives;
using SmartEdu.Modules.SessionModule.Adapters;

namespace SmartEdu.Modules.SessionModule.Core
{
    public class CheckTokenValidationMiddleware
    {
        public CheckTokenValidationMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        private readonly RequestDelegate _next;

        public async Task InvokeAsync(HttpContext httpContext, SessionService sessionService)
        {
            // automatic check of access token validation
            if (httpContext.Request.Path != "/session/refresh")
            {
                if (httpContext.Request.Headers["Authorization"] != StringValues.Empty)
                {
                    if (await sessionService.CheckAccessTokenValidation(httpContext.Request.Headers["Authorization"]!))
                    {
                        httpContext.Request.Headers["Authorization"] = StringValues.Empty;
                    }
                }
            }

            await _next.Invoke(httpContext);
        }
    }

    public static class CheckTokenValidationExtensions
    {
        public static IApplicationBuilder UseTokenValidationChecking(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckTokenValidationMiddleware>();
        }
    }
}
