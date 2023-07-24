using SmartEdu.Modules.CourseModule.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartEdu.Modules.CourseModule.Converters
{
    public class UpdateCoordsCourseElementJsonConverter : JsonConverter<UpdateCoordsCourseElementDTO>
    {
        public override UpdateCoordsCourseElementDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            int? elementId = null;
            string? coords = null;

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
                        case "Coords":
                            coords = reader.GetString();
                            break;
                    }
                }
            }

            if (elementId == null)
                return null;
            else
                return new UpdateCoordsCourseElementDTO(elementId, coords);
        }

        public override void Write(Utf8JsonWriter writer, UpdateCoordsCourseElementDTO registrationData, JsonSerializerOptions options)
        {
        }
    }
}
