using System.Text.Json;
using SmartEdu.Modules.SessionModule.Adapters;
using SmartEdu.Modules.SessionModule.Converters;
using SmartEdu.Modules.SessionModule.DTO;

namespace SmartEdu.Modules.SessionModule.Endpoints
{
    public static partial class SessionEndpoints
    {
        public async static Task DeleteSession(HttpContext httpContext, SessionService sessionService)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new DeleteSessionJsonConverter());

                // get tokens
                var tokens = await httpContext.Request.ReadFromJsonAsync<DeleteSessionDTO>(jsonOptions);

                if(tokens != null)
                {
                    if (await sessionService.CheckRefreshTokenValidation(httpContext.Request.Headers["Authorization"]!))
                    {
                        if (await sessionService.CheckAccessTokenValidation(tokens.accessToken!))
                        {
                            sessionService.InvalidateTokens(httpContext.Request.Headers["Authorization"]!, tokens.accessToken!);
                        }
                        else
                        {
                            await Results.Unauthorized().ExecuteAsync(httpContext);
                        }
                    }
                    else
                    {
                        await Results.Unauthorized().ExecuteAsync(httpContext);
                    }
                }
                else
                {
                    await Results.BadRequest(new { message = "Request hasn't access token" }).ExecuteAsync(httpContext);
                }
            }
            else
            {
                await Results.UnprocessableEntity().ExecuteAsync(httpContext);
            }
        }
    }
}
