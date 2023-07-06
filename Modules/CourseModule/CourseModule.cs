namespace SmartEdu.Modules.CourseModule
{
    public class CourseModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            return endpoints;
        }
    }
}
