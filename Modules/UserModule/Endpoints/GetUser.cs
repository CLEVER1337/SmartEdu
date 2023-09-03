using SmartEdu.Modules.SessionModule.Adapters;
using SmartEdu.Modules.UserModule.Converters;
using SmartEdu.Modules.UserModule.Core;
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

                if (token["discriminator"] == "Tutor")
                {
                    var tutor = await TutorCreator.GetTutor(Convert.ToInt32(token["userId"]));

                    var coursesPreviews = new GetTutorDTOCoursePreview[tutor!.OwnedCourses.Count];

                    jsonOptions.Converters.Add(new GetTutorJsonConverter());

                    foreach (var course in tutor!.OwnedCourses)
                    {
                        coursesPreviews.Append(new GetTutorDTOCoursePreview(course.Cost, course.Rating, course.Difficulty));
                    }

                    await httpContext.Response.WriteAsJsonAsync(new GetTutorDTO(user.UserData.Login, coursesPreviews), jsonOptions);
                }
                else if(token["discriminator"] == "Student")
                {

                }
            }
            else
            {
                await Results.Unauthorized().ExecuteAsync(httpContext);
            }
        }
    }
}
