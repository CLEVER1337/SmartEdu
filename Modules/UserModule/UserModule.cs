using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SmartEdu.Modules.HashingModule.Ports;
using SmartEdu.Modules.RegistrationModule.Ports;
using SmartEdu.Modules.SessionModule.Adapters;
using SmartEdu.Modules.UserModule.Endpoints;

namespace SmartEdu.Modules.UserModule
{
    public class UserModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services) 
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) 
        {
            //endpoints.MapDelete();
            endpoints.MapGet("user/get",
                [Authorize]
                (HttpContext httpContext, SessionService sessionService)
                => UserEndpoints.GetUser(httpContext, sessionService));
            endpoints.MapPost("user/{userType}/register", 
                (HttpContext httpContext, IRegistrationService registrationService, IHashService hashService, string userType) 
                => UserEndpoints.RegisterUser(httpContext, registrationService, hashService, userType));
            //endpoints.MapPost("/login", () => UserEndpoints.RegisterUser());
            //endpoints.MapPut();

            // different profiles for different user types
            endpoints.Map("profile",
            async (HttpContext httpContext, SessionService sessionService) =>
            {
                if (httpContext.Request.Cookies["jwtBearer"] != null)
                {
                    // get user with JWT's id
                    var token = sessionService.DecodeToken(httpContext.Request.Cookies["jwtBearer"]!);

                    if (token["discriminator"] == "Tutor")
                        await httpContext.Response.SendFileAsync("wwwroot/documents/PersonalAccountTutor.html");
                    else if (token["discriminator"] == "Student")
                        await httpContext.Response.SendFileAsync("documents/PersonalAccountStudent.html");
                }
                else
                {
                    await Results.Unauthorized().ExecuteAsync(httpContext);
                }
            });

            return endpoints;
        }
    }
}
