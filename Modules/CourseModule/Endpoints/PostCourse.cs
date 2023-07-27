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

                // Get exercise data
                var courseData = await httpContext.Request.ReadFromJsonAsync<CreateCourseDTO>(jsonOptions);

                if (courseData != null)
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

        public async static Task CreateExercise(HttpContext httpContext)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new CreateCourseExerciseJsonConverter());

                // Get exercise data
                var exerciseData = await httpContext.Request.ReadFromJsonAsync<CreateCourseExerciseDTO>(jsonOptions);

                if (exerciseData != null)
                {
                    var exercise = new CourseExercise(exerciseData.name!);

                    await CourseExercise.Save(exercise);
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

                // Get exercise element data
                var exerciseElementData = await httpContext.Request.ReadFromJsonAsync<CreateCourseElementDTO>(jsonOptions);

                if (exerciseElementData != null)
                {
                    var courseBuilder = new CourseBuilder(exerciseElementData.courseId!.Value);

                    switch (exerciseElementData.discriminator)
                    {
                        case "Page":
                            courseBuilder.BuildPage(exerciseElementData.courseExerciseId!.Value); 
                            break;
                        case "Text":
                            courseBuilder.BuildElement<CourseTextElement>(exerciseElementData.courseExerciseId!.Value, 
                                                                                        exerciseElementData.exercisePageId!.Value, 
                                                                                        new Coord(exerciseElementData.coords!));
                            break;
                        case "Image":
                            courseBuilder.BuildElement<CourseImageElement>(exerciseElementData.courseExerciseId!.Value, 
                                                                                        exerciseElementData.exercisePageId!.Value, 
                                                                                        new Coord(exerciseElementData.coords!));
                            break;
                        case "AnswerField":
                            courseBuilder.BuildElement<CourseAnswerFieldElement>(exerciseElementData.courseExerciseId!.Value, 
                                                                                        exerciseElementData.exercisePageId!.Value, 
                                                                                        new Coord(exerciseElementData.coords!));
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
