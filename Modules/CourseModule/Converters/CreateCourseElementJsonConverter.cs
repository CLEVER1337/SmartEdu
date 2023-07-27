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
            int? exerciseId = null;
            int? courseId = null;
            int? pageId = null;
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
                            discriminator = reader.GetString();
                            break;
                        case "CourseId":
                            courseId = reader.GetInt32();
                            break;
                        case "ExercisePageId":
                            pageId = reader.GetInt32();
                            break;
                        case "ExerciseId":
                            exerciseId = reader.GetInt32();
                            break;
                        case "Coords":
                            coords = reader.GetString();
                            break;
                    }
                }
            }

            if (discriminator == null 
                && pageId == null 
                && exerciseId == null)
                return null;
            else
                return new CreateCourseElementDTO(discriminator, courseId, exerciseId, pageId, coords);
        }

        public override void Write(Utf8JsonWriter writer, CreateCourseElementDTO registrationData, JsonSerializerOptions options)
        {
        }
    }
}
