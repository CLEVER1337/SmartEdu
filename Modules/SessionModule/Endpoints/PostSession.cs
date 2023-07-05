using SmartEdu.Modules.HashingModule.Ports;
using SmartEdu.Modules.LoginModule.Converters;
using SmartEdu.Modules.LoginModule.Core;
using SmartEdu.Modules.SessionModule.Adapters;
using SmartEdu.Modules.SessionModule.Core;
using SmartEdu.Modules.UserModule.Factory;
using System.Security.Claims;
using System.Text.Json;

namespace SmartEdu.Modules.SessionModule.Endpoints
{
    public static partial class SessionEndpoints
    {
        // Login POST request
        public async static Task CreateSession(HttpContext httpContext, SessionService sessionService, IHashService hashService)
        {
            if(httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new LoginJsonConverter());

                // Get login data from request
                var loginData = await httpContext.Request.ReadFromJsonAsync<LoginData>(jsonOptions);

                if (loginData != null)
                {
                    var user = UserCreator.GetUser(loginData.login!);

                    if (user != null)
                    {
                        hashService.HashFunction(loginData.password!, user.UserData.Salt);

                        if (hashService.Hash == user.UserData.HashedPassword)
                        {
                            var refreshTokenClaims = new List<Claim> 
                            {
                                new Claim("userId", user.Id.ToString())
                            };
                            var accessTokenClaims = new List<Claim> 
                            {
                                new Claim("userId", user.Id.ToString())
                            };

                            var tokensData = new TokensData(sessionService.CreateRefreshToken(refreshTokenClaims), sessionService.CreateAccessToken(accessTokenClaims, TimeSpan.FromDays(3)));

                            await httpContext.Response.WriteAsJsonAsync<TokensData>(tokensData);
                        }
                        else
                        {
                            await Results.BadRequest(new { message = "Wrong password" }).ExecuteAsync(httpContext);
                        }
                    }
                    else
                    {
                        await Results.NotFound(new { message = "Not registered" }).ExecuteAsync(httpContext);
                    }
                }
                else
                {
                    await Results.UnprocessableEntity(new { message = "Request hasn't login or password" }).ExecuteAsync(httpContext);
                }
            }
            else
            {
                await Results.UnprocessableEntity().ExecuteAsync(httpContext);
            }
        }
    }
}
