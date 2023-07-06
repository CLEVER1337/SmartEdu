using System.Text.Json;
using SmartEdu.Modules.SessionModule.Adapters;
using SmartEdu.Modules.SessionModule.Converters;
using SmartEdu.Modules.SessionModule.Core;

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

                jsonOptions.Converters.Add(new TokensJsonConverter());

                // get tokens
                var tokens = await httpContext.Request.ReadFromJsonAsync<TokensData>(jsonOptions);

                if(tokens != null)
                {
                    if (await sessionService.CheckRefreshTokenValidation(tokens.refreshToken!))
                    {
                        if (await sessionService.CheckAccessTokenValidation(tokens.accessToken!))
                        {
                            sessionService.InvalidateTokens(tokens.refreshToken!, tokens.accessToken!);
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
                    await Results.BadRequest(new { message = "Request hasn't tokens" }).ExecuteAsync(httpContext);
                }
            }
            else
            {
                await Results.UnprocessableEntity().ExecuteAsync(httpContext);
            }
        }
    }
}
