using SmartEdu.Modules.SessionModule.Adapters;
using SmartEdu.Modules.SessionModule.Converters;
using SmartEdu.Modules.SessionModule.Core;
using System.Text.Json;

namespace SmartEdu.Modules.SessionModule.Endpoints
{
    public static partial class SessionEndpoints
    {
        public async static Task RefreshTokens(HttpContext httpContext, SessionService sessionService)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new TokensJsonConverter());

                var tokens = await httpContext.Request.ReadFromJsonAsync<TokensData>(jsonOptions);

                if(tokens != null)
                {
                    var refreshedTokens = sessionService.RefreshTokens(httpContext.Request.Headers["Authorization"]!, tokens.accessToken!);

                    await httpContext.Response.WriteAsJsonAsync<TokensData>(refreshedTokens);
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
