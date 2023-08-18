using SmartEdu.Modules.SessionModule.Adapters;
using SmartEdu.Modules.UserModule.Converters;
using SmartEdu.Modules.UserModule.DTO;
using SmartEdu.Modules.UserModule.Factory;
using System.Text.Json;

namespace SmartEdu.Modules.UserModule.Endpoints
{
    public static partial class UserEndpoints
    {
        /// <summary>
        /// Get user request
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="sessionService"></param>
        /// <returns></returns>
        public async static Task GetUser(HttpContext httpContext, SessionService sessionService) 
        {
            // get user with JWT's id
            var token = sessionService.DecodeToken(httpContext.Request.Headers["Authorization"]!);

            var user = await UserCreator.GetUser(Convert.ToInt32(token["userId"]));

            if (user != null) 
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new GetUserJsonConverter());

                var userData = new GetUserDTO(user.UserData.Login);

                await httpContext.Response.WriteAsJsonAsync<GetUserDTO>(userData, jsonOptions);
            }
            else
            {
                await Results.Unauthorized().ExecuteAsync(httpContext);
            }
        }
    }
}
