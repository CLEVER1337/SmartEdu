using Microsoft.AspNetCore.Http;
using SmartEdu.Modules.CourseModule.Builder;
using SmartEdu.Modules.CourseModule.Converters;
using SmartEdu.Modules.CourseModule.Core;
using SmartEdu.Modules.CourseModule.DecoratorElements;
using System.Text.Json;

namespace SmartEdu.Modules.CourseModule.Endpoints
{
    public static partial class CourseEndpoints
    {
        public async static Task SetCoords(HttpContext httpContext)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new CourseElementJsonConverter());

                // Get course element data
                var courseElementData = await httpContext.Request.ReadFromJsonAsync<CourseElementData>(jsonOptions);

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

                                await CoursePageElement.Save(element);
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

        public async static Task SetText(HttpContext httpContext)
        {
            if (httpContext.Request.HasJsonContentType())
            {
                // Set json converter
                var jsonOptions = new JsonSerializerOptions();

                jsonOptions.Converters.Add(new CourseElementJsonConverter());

                // Get course element data
                var courseElementData = await httpContext.Request.ReadFromJsonAsync<CourseElementData>(jsonOptions);

                if (courseElementData != null)
                {
                    if (courseElementData.elementId != null
                       && courseElementData.value != null)
                    {
                        var element = await CourseBuilder.GetPageElement<CoursePageTextElement>(courseElementData.elementId.Value);

                        if (element != null)
                        {
                            using (var context = new ApplicationContext())
                            {
                                context.CoursePageElements.Update(element);

                                element.Text = courseElementData.value;

                                await CoursePageElement.Save(element);
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

        public async static Task SetImage(HttpContext httpContext)
        {

        }

        public async static Task SetTheme(HttpContext httpContext)
        {

        }
    }
}
