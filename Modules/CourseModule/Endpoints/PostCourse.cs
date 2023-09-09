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
        /// <summary>
        /// Create course
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="sessionService"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create exercise of course
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
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
                    var builder = new CourseBuilder(exerciseData.courseId!.Value);

                    builder.BuildExercise(exerciseData.name!);

                    //var exercise = new CourseExercise(exerciseData.name!);

                    //await CourseExercise.Save(exercise);
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

        /// <summary>
        /// Create exercise's page
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async static Task CreatePage(HttpContext httpContext)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new CreatePageJsonConverter());

                // Get exercise element data
                var exerciseElementData = await httpContext.Request.ReadFromJsonAsync<CreatePageDTO>(jsonOptions);

                if (exerciseElementData != null)
                {
                    var courseBuilder = new CourseBuilder(exerciseElementData.courseId!.Value);

                    courseBuilder.BuildPage(exerciseElementData.courseExerciseId!.Value);
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

        /// <summary>
        /// Create text element
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async static Task CreateTextElement(HttpContext httpContext)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new CreateTextElementJsonConverter());

                // Get exercise element data
                var exerciseElementData = await httpContext.Request.ReadFromJsonAsync<CreateTextElementDTO>(jsonOptions);

                if (exerciseElementData != null)
                {
                    var courseBuilder = new CourseBuilder(exerciseElementData.courseId!.Value);

                    courseBuilder.BuildElement<CourseTextElement>(exerciseElementData.courseExerciseId!.Value,
                        exerciseElementData.exercisePageId!.Value, new Coord(exerciseElementData.coords!));
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

        /// <summary>
        /// Create Image
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async static Task CreateImageElement(HttpContext httpContext)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new CreateImageElementJsonConverter());

                // Get exercise element data
                var exerciseElementData = await httpContext.Request.ReadFromJsonAsync<CreateImageElementDTO>(jsonOptions);

                if (exerciseElementData != null)
                {
                    var courseBuilder = new CourseBuilder(exerciseElementData.courseId!.Value);

                    courseBuilder.BuildElement<CourseImageElement>(exerciseElementData.courseExerciseId!.Value,
                        exerciseElementData.exercisePageId!.Value, new Coord(exerciseElementData.coords!));
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

        /// <summary>
        /// Create answer field
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async static Task CreateAnswerFieldElement(HttpContext httpContext)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new CreateAnswerFieldElementJsonConverter());

                // Get exercise element data
                var exerciseElementData = await httpContext.Request.ReadFromJsonAsync<CreateAnswerFieldDTO>(jsonOptions);

                if (exerciseElementData != null)
                {
                    var courseBuilder = new CourseBuilder(exerciseElementData.courseId!.Value);

                    courseBuilder.BuildElement<CourseAnswerFieldElement>(exerciseElementData.courseExerciseId!.Value,
                        exerciseElementData.exercisePageId!.Value, new Coord(exerciseElementData.coords!));
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
