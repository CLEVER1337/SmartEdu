using SmartEdu.Modules.CourseModule.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartEdu.Modules.CourseModule.Converters
{
    /// <summary>
    /// Set coords convetrer
    /// </summary>
    public class UpdateCourseElementCoordsJsonConverter : JsonConverter<UpdateCourseElementCoordsDTO>
    {
        public override UpdateCourseElementCoordsDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                return new UpdateCourseElementCoordsDTO(elementId, coords);
        }

        public override void Write(Utf8JsonWriter writer, UpdateCourseElementCoordsDTO registrationData, JsonSerializerOptions options)
        {
        }
    }
}
