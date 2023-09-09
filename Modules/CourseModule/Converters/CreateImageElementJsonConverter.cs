using SmartEdu.Modules.CourseModule.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartEdu.Modules.CourseModule.Converters
{
    public class CreateImageElementJsonConverter :  JsonConverter<CreateImageElementDTO>
    {
        public override CreateImageElementDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
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

            if (exerciseId != null &&
                courseId != null &&
                pageId != null &&
                coords != null)
                return new CreateImageElementDTO(courseId, exerciseId, pageId, coords);
            else
                return null;
        }

        public override void Write(Utf8JsonWriter writer, CreateImageElementDTO registrationData, JsonSerializerOptions options)
        {
        }
    }
}
