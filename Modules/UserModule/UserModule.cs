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
            //endpoints.MapPost();
            //endpoints.MapPut();
            return endpoints;
        }
    }
}
