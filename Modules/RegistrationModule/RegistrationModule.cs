using SmartEdu.Modules.RegistrationModule.Adapters;
using SmartEdu.Modules.RegistrationModule.Ports;

namespace SmartEdu.Modules.RegistrationModule
{
    public class RegistrationModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddTransient<IRegistrationService, UserRegistrationService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            return endpoints;
        }
    }
}
