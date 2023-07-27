using SmartEdu.Modules.CourseModule.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartEdu.Modules.CourseModule.Converters
{
    public class CreateCourseElementJsonConverter : JsonConverter<CreateCourseElementDTO>
    {
        public override CreateCourseElementDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? discriminator = null;
            int? courseId = null;
            int? coursePageId = null;
            string? coords = null;

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
                        case "Coords":
                            coords = reader.GetString();
                            break;
                    }
                }
            }

            if (discriminator == null 
                && coursePageId == null 
                && courseId == null)
                return null;
            else
                return new CreateCourseElementDTO(discriminator, courseId, coursePageId, coords);
        }

        public override void Write(Utf8JsonWriter writer, CreateCourseElementDTO registrationData, JsonSerializerOptions options)
        {
        }
    }
}
