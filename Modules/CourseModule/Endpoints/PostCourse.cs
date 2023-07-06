using SmartEdu.Modules.CourseModule.Builder;
using SmartEdu.Modules.CourseModule.Converters;
using SmartEdu.Modules.CourseModule.Core;
using SmartEdu.Modules.HashingModule.Ports;
using SmartEdu.Modules.RegistrationModule.Converters;
using SmartEdu.Modules.RegistrationModule.Core;
using SmartEdu.Modules.RegistrationModule.Ports;
using SmartEdu.Modules.UserModule.Factory;
using System.Text.Json;

namespace SmartEdu.Modules.CourseModule.Endpoints
{
    public static partial class CourseEndpoints
    {
        public async static Task CreateCourse(HttpContext httpContext)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new CourseJsonConverter());

                // Get course data
                var courseData = await httpContext.Request.ReadFromJsonAsync<CourseData>(jsonOptions);

                if(courseData != null)
                {
                    var course = new Course(courseData.name!);

                    await Course.Save(course);
                }
                else
                {
                    await Results.UnprocessableEntity(new { message = "Request hasn't required data" }).ExecuteAsync(httpContext);
                }
            }
            else
            {
                await Results.UnprocessableEntity().ExecuteAsync(httpContext);
            }
        }
    }
}
