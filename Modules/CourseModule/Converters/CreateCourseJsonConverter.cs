using SmartEdu.Modules.CourseModule.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartEdu.Modules.CourseModule.Converters
{
    public class CreateCourseJsonConverter : JsonConverter<CreateCourseDTO>
    {
        public override CreateCourseDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? name = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString()!;
                    reader.Read();
                    switch (propertyName?.ToLower())
                    {
                        case "Name":
                            name = reader.GetString()!;
                            break;
                    }
                }
            }

            if (name == null)
                return null;
            else
                return new CreateCourseDTO(name);
        }

        public override void Write(Utf8JsonWriter writer, CreateCourseDTO registrationData, JsonSerializerOptions options)
        {
        }
    }
}
