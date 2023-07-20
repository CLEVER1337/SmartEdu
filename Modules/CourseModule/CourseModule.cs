using Microsoft.AspNetCore.Authorization;
using SmartEdu.Modules.CourseModule.Endpoints;
using SmartEdu.Modules.SessionModule.Adapters;

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
            endpoints.MapPost("course/create",
                [Authorize]
                (HttpContext httpContext, SessionService sessionService) => CourseEndpoints.CreateCourse(httpContext, sessionService));
            endpoints.MapPost("course/element/create",
                [Authorize]
                (HttpContext httpContext) => CourseEndpoints.CreateElement(httpContext));

            return endpoints;
        }
    }
}
