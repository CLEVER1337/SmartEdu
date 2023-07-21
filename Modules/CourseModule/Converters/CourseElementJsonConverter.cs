using SmartEdu.Modules.CourseModule.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartEdu.Modules.CourseModule.Converters
{
    public class CourseElementJsonConverter : JsonConverter<CourseElementData>
    {
        public override CourseElementData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? discriminator = null;
            int? coursePageId = null;
            int? courseId = null;
            int? elementId = null;
            string? coords = null;
            string? value = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString()!;
                    reader.Read();
                    switch (propertyName?.ToLower())
                    {
                        case "Discriminator":
                            discriminator = reader.GetString()!;
                            break;
                        case "CoursePageId":
                            coursePageId = reader.GetInt32();
                            break;
                        case "CourseId":
                            courseId = reader.GetInt32();
                            break;
                        case "ElementId":
                            elementId = reader.GetInt32();
                            break;
                        case "Coords":
                            coords = reader.GetString();
                            break;
                        case "Value":
                            value = reader.GetString();
                            break;
                    }
                }
            }

            if (discriminator == null 
                && coursePageId == null 
                && courseId == null 
                && coords == null 
                && value == null)
                return null;
            else
                return new CourseElementData(discriminator, courseId, coursePageId, elementId, coords, value);
        }

        public override void Write(Utf8JsonWriter writer, CourseElementData registrationData, JsonSerializerOptions options)
        {
        }
    }
}
