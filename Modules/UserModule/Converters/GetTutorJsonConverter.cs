using SmartEdu.Modules.UserModule.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartEdu.Modules.UserModule.Converters
{
    /// <summary>
    /// Json converter for get user
    /// </summary>
    public class GetTutorJsonConverter : JsonConverter<GetTutorDTO>
    {
        public override GetTutorDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return null;
        }

        public override void Write(Utf8JsonWriter writer, GetTutorDTO registrationData, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("Login", registrationData.login);
            
            writer.WriteStartArray("CoursePreviews");

            foreach(var coursePreview in registrationData.CoursePreviews)
            {
                writer.WriteStartObject();

                writer.WriteNumber("Cost", coursePreview.cost!.Value);
                writer.WriteNumber("Rating", coursePreview.rating!.Value);
                writer.WriteNumber("Difficulty", coursePreview.difficulty!.Value);

                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}
