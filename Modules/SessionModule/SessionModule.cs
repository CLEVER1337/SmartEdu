using Microsoft.AspNetCore.Authorization;
using SmartEdu.Modules.HashingModule.Ports;
using SmartEdu.Modules.SessionModule.Adapters;
using SmartEdu.Modules.SessionModule.Endpoints;

namespace SmartEdu.Modules.SessionModule
{
    public class SessionModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddTransient<SessionService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapDelete("session/delete", 
                (HttpContext httpContext, SessionService sessionService)
                => SessionEndpoints.DeleteSession(httpContext, sessionService));
            endpoints.MapPost("session/create", 
                (HttpContext httpContext, SessionService sessionService, IHashService hashService) 
                => SessionEndpoints.CreateSession(httpContext, sessionService, hashService));
            endpoints.MapPut("session/refresh",
                [Authorize]
                (HttpContext httpContext, SessionService sessionService)
                => SessionEndpoints.RefreshTokens(httpContext, sessionService));

            return endpoints;
        }
    }
}
