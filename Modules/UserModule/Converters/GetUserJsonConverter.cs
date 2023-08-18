using SmartEdu.Modules.UserModule.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartEdu.Modules.UserModule.Converters
{
    /// <summary>
    /// Json converter for get user
    /// </summary>
    public class GetUserJsonConverter : JsonConverter<GetUserDTO>
    {
        public override GetUserDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return null;
        }

        public override void Write(Utf8JsonWriter writer, GetUserDTO registrationData, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("login", registrationData.login);
            
            writer.WriteEndObject();
        }
    }
}
