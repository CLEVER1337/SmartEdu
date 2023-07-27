using System.Text.Json;
using System.Text.Json.Serialization;
using SmartEdu.Modules.CourseModule.DTO;

namespace SmartEdu.Modules.CourseModule.Converters
{
    public class UpdateCourseElementValueJsonConverter : JsonConverter<UpdateCourseElementValueDTO>
    {
        public override UpdateCourseElementValueDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            int? elementId = null;
            string? value = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString()!;
                    reader.Read();
                    switch (propertyName?.ToLower())
                    {
                        case "ElementId":
                            elementId = reader.GetInt32();
                            break;
                        case "Value":
                            value = reader.GetString();
                            break;
                    }
                }
            }

            if (elementId == null)
                return null;
            else
                return new UpdateCourseElementValueDTO(elementId, value);
        }

        public override void Write(Utf8JsonWriter writer, UpdateCourseElementValueDTO registrationData, JsonSerializerOptions options)
        {
        }
    }
}
