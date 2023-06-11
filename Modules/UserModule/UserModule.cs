using SmartEdu.Modules.HashingModule.Ports;
using SmartEdu.Modules.RegistrationModule.Ports;
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
            //endpoints.MapGet();
            /*endpoints.MapPost("/registration/{userType}", 
                (HttpContext httpContext, IRegistrationService registrationService, IHashService hashService, string userType) 
                => UserEndpoints.PostUser(httpContext, registrationService, hashService, userType));*/
            //endpoints.MapPost("/login", () => UserEndpoints.PostUser());
            //endpoints.MapPut();
            return endpoints;
        }
    }
}
