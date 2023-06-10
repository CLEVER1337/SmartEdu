using System.Text.Json;
using SmartEdu.Modules.HashingModule.Ports;
using SmartEdu.Modules.RegistrationModule.Converters;
using SmartEdu.Modules.RegistrationModule.Core;
using SmartEdu.Modules.RegistrationModule.Ports;
using SmartEdu.Modules.UserModule.Factory;

namespace SmartEdu.Modules.UserModule.Endpoints
{
    public static partial class UserEndpoints
    {
        public async static Task PostUser(HttpContext httpContext, IRegistrationService registrationService, IHashService hashService, string userType)
        {
            UserCreator userCreator = null!;

            if (userType.ToLower() == "student")
            {
                userCreator = new StudentCreator();
            }
            else if (userType.ToLower() == "tutor")
            {
                userCreator = new TutorCreator();
            }
            else
            {
                await Results.UnprocessableEntity(new { message = "Wrong user type" }).ExecuteAsync(httpContext);
                return;
            }

            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new RegistrationJsonConverter());

                // Get registration data from request
                var registrationData = await httpContext.Request.ReadFromJsonAsync<RegistrationData>(jsonOptions);

                if (registrationData == null)
                {
                    await Results.UnprocessableEntity(new {message = "Request hasn't login or password"}).ExecuteAsync(httpContext);
                }
                else
                {
                    hashService.HashFunction(registrationData.password!);

                    var user = registrationService.Register(registrationData.login!, hashService.Salt, hashService.Hash, userCreator);

                    if (user == null)
                        await Results.BadRequest(new { message = "This login is already in use of another user" }).ExecuteAsync(httpContext);
                    else
                        await Results.Ok(user.Id).ExecuteAsync(httpContext);
                }
            }
            else
            {
                await Results.UnprocessableEntity().ExecuteAsync(httpContext);
            }
        }
    }
}
