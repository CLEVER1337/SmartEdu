namespace SmartEdu.Modules.ErrorHandlingModule
{
    public class ErrorHandlingModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.Map("error/{statusCode}", (int statusCode) => 
            {
                return $"Http error occured. Status code is: {statusCode}";
            });

            return endpoints;
        }
    }
}
