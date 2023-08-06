using SmartEdu.Modules.HashingModule.Ports;
using SmartEdu.Modules.LoginModule.Converters;
using SmartEdu.Modules.LoginModule.Core;
using SmartEdu.Modules.SessionModule.Adapters;
using SmartEdu.Modules.SessionModule.DTO;
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

                jsonOptions.Converters.Add(new CreateSessionJsonConverter());

                // Get login data from request
                var loginData = await httpContext.Request.ReadFromJsonAsync<LoginModule.Core.CreateSessionDTO>(jsonOptions);

                if (loginData != null)
                {
                    // check does this user exist
                    var user = await UserCreator.GetUser(loginData.login!);

                    if (user != null)
                    {
                        // hash gotten password
                        hashService.HashFunction(loginData.password!, user.UserData.Salt);

                        //check hashs
                        if (hashService.Hash == user.UserData.HashedPassword)
                        {
                            // create access and refresh tokens
                            var refreshTokenClaims = new List<Claim> 
                            {
                                new Claim("userId", user.Id.ToString())
                            };
                            var accessTokenClaims = new List<Claim> 
                            {
                                new Claim("userId", user.Id.ToString())
                            };

                            var tokensData = new DTO.CreateSessionDTO(await sessionService.CreateRefreshToken(refreshTokenClaims), sessionService.CreateAccessToken(accessTokenClaims, TimeSpan.FromDays(3)));

                            await httpContext.Response.WriteAsJsonAsync(tokensData);
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
