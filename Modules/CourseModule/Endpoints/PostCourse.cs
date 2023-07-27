using SmartEdu.Modules.CourseModule.Builder;
using SmartEdu.Modules.CourseModule.Converters;
using SmartEdu.Modules.CourseModule.Core;
using SmartEdu.Modules.CourseModule.DecoratorElements;
using SmartEdu.Modules.CourseModule.DTO;
using SmartEdu.Modules.SessionModule.Adapters;
using SmartEdu.Modules.UserModule.Factory;
using System.Text.Json;

namespace SmartEdu.Modules.CourseModule.Endpoints
{
    public static partial class CourseEndpoints
    {
        public async static Task CreateCourse(HttpContext httpContext, SessionService sessionService)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new CreateCourseJsonConverter());

                // Get course data
                var courseData = await httpContext.Request.ReadFromJsonAsync<CreateCourseDTO>(jsonOptions);

                if(courseData != null)
                {
                    var course = new Course(courseData.name!);

                    var tutor = await TutorCreator.GetTutor(
                        Convert.ToInt32(
                            sessionService.DecodeToken(
                                httpContext.Request.Headers["Authorization"]!)["userId"]));

                    course.Author = tutor;

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

        public async static Task CreateElement(HttpContext httpContext)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new CreateCourseElementJsonConverter());

                // Get course element data
                var courseElementData = await httpContext.Request.ReadFromJsonAsync<CreateCourseElementDTO>(jsonOptions);

                if (courseElementData != null)
                {
                    var courseBuilder = new CourseBuilder(courseElementData.courseId!.Value);

                    switch (courseElementData.discriminator)
                    {
                        case "CoursePage":
                            courseBuilder.BuildPage(); 
                            break;
                        case "CoursePageTextElement":
                            courseBuilder.BuildElement<CoursePageTextElement>(courseElementData.coursePageId!.Value, new Coord(courseElementData.coords!));
                            break;
                        case "CoursePageImageElement":
                            courseBuilder.BuildElement<CoursePageImageElement>(courseElementData.coursePageId!.Value, new Coord(courseElementData.coords!));
                            break;
                        case "CoursePageAnswerFieldElement":
                            courseBuilder.BuildElement<CoursePageAnswerFieldElement>(courseElementData.coursePageId!.Value, new Coord(courseElementData.coords!));
                            break;
                    }
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
