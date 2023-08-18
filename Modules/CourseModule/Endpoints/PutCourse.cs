using SmartEdu.Modules.CourseModule.Builder;
using SmartEdu.Modules.CourseModule.Converters;
using SmartEdu.Modules.CourseModule.DecoratorElements;
using SmartEdu.Modules.CourseModule.DTO;
using System.Text.Json;

namespace SmartEdu.Modules.CourseModule.Endpoints
{
    public static partial class CourseEndpoints
    {
        /// <summary>
        /// Set coords to element of exercise
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async static Task SetCoords(HttpContext httpContext)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new UpdateCourseElementCoordsJsonConverter());

                // Get course element data
                var courseElementData = await httpContext.Request.ReadFromJsonAsync<UpdateCourseElementCoordsDTO>(jsonOptions);

                if (courseElementData != null)
                {
                    if(courseElementData.elementId != null
                       && courseElementData.coords != null)
                    {
                        var element = await CourseBuilder.GetPageElement(courseElementData.elementId.Value);

                        if (element != null)
                        {
                            using (var context = new ApplicationContext())
                            {
                                context.CoursePageElements.Update(element);

                                element.Coords = courseElementData.coords;

                                await CourseElement.Save(element);
                            }
                        }
                        else
                        {
                            await Results.BadRequest(new { message = "Element with this id doesn't exist" }).ExecuteAsync(httpContext);
                        }
                    }
                    else
                    {
                        await Results.UnprocessableEntity(new { message = "Request hasn't required data" }).ExecuteAsync(httpContext);
                    }
                }
                else
                {
                    await Results.UnprocessableEntity(new { message = "Request hasn't any data" }).ExecuteAsync(httpContext);
                }
            }
            else
            {
                await Results.UnprocessableEntity().ExecuteAsync(httpContext);
            }
        }

        /// <summary>
        /// Set text to text element
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async static Task SetText(HttpContext httpContext)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new UpdateCourseElementValueJsonConverter());

                // Get course element data
                var courseElementData = await httpContext.Request.ReadFromJsonAsync<UpdateCourseElementValueDTO>(jsonOptions);

                if (courseElementData != null)
                {
                    if (courseElementData.elementId != null
                       && courseElementData.value != null)
                    {
                        var element = await CourseBuilder.GetPageElement<CourseTextElement>(courseElementData.elementId.Value);

                        if (element != null)
                        {
                            using (var context = new ApplicationContext())
                            {
                                context.CoursePageElements.Update(element);

                                element.Text = courseElementData.value;

                                await CourseElement.Save(element);
                            }
                        }
                        else
                        {
                            await Results.BadRequest(new { message = "Element with this id doesn't exist" }).ExecuteAsync(httpContext);
                        }
                    }
                    else
                    {
                        await Results.UnprocessableEntity(new { message = "Request hasn't required data" }).ExecuteAsync(httpContext);
                    }
                }
                else
                {
                    await Results.UnprocessableEntity(new { message = "Request hasn't any data" }).ExecuteAsync(httpContext);
                }
            }
            else
            {
                await Results.UnprocessableEntity().ExecuteAsync(httpContext);
            }
        }

        /// <summary>
        /// Set image's path to image element
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async static Task SetImage(HttpContext httpContext)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new UpdateCourseElementValueJsonConverter());

                // Get course element data
                var courseElementData = await httpContext.Request.ReadFromJsonAsync<UpdateCourseElementValueDTO>(jsonOptions);

                if (courseElementData != null)
                {
                    if (courseElementData.elementId != null
                       && courseElementData.value != null)
                    {
                        var element = await CourseBuilder.GetPageElement<CourseImageElement>(courseElementData.elementId.Value);

                        if (element != null)
                        {
                            using (var context = new ApplicationContext())
                            {
                                context.CoursePageElements.Update(element);

                                element.ImageName = courseElementData.value;

                                await CourseElement.Save(element);
                            }
                        }
                        else
                        {
                            await Results.BadRequest(new { message = "Element with this id doesn't exist" }).ExecuteAsync(httpContext);
                        }
                    }
                    else
                    {
                        await Results.UnprocessableEntity(new { message = "Request hasn't required data" }).ExecuteAsync(httpContext);
                    }
                }
                else
                {
                    await Results.UnprocessableEntity(new { message = "Request hasn't any data" }).ExecuteAsync(httpContext);
                }
            }
            else
            {
                await Results.UnprocessableEntity().ExecuteAsync(httpContext);
            }
        }

        /// <summary>
        /// Set exercise's theme
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async static Task SetTheme(HttpContext httpContext)
        {
            Results.BadRequest();
        }
    }
}
