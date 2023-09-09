using SmartEdu.Modules.CourseModule.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartEdu.Modules.CourseModule.Converters
{
    public class CreatePageJsonConverter : JsonConverter<CreatePageDTO>
    {
        public override CreatePageDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            int? exerciseId = null;
            int? courseId = null;

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
                        case "ExerciseId":
                            exerciseId = reader.GetInt32();
                            break;
                    }
                }
            }

            if (exerciseId != null &&
                courseId != null)
                return new CreatePageDTO(courseId, exerciseId);
            else
                return null;
        }

        public override void Write(Utf8JsonWriter writer, CreatePageDTO registrationData, JsonSerializerOptions options)
        {
        }
    }
}
