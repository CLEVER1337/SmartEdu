using Microsoft.Extensions.Primitives;
using SmartEdu.Modules.SessionModule.Adapters;

namespace SmartEdu.Modules.UserModule.Endpoints
{
    public static partial class UserEndpoints
    {
        public async static Task GetUser(HttpContext httpContext, SessionService sessionService) 
        {
            if (httpContext.Request.Headers["Authorization"] != StringValues.Empty)
            {

            }
            else
            {
                await Results.Unauthorized().ExecuteAsync(httpContext);
            }
        }
    }
}
