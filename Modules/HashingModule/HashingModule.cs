using SmartEdu.Modules.HashingModule.Adapters;
using SmartEdu.Modules.HashingModule.Ports;

namespace SmartEdu.Modules.HashingModule
{
    public class HashingModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddTransient<IHashService, HashSHA256Service>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            return endpoints;
        }
    }
}
