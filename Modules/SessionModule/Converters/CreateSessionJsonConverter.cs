using System.Text.Json;
using System.Text.Json.Serialization;
using SmartEdu.Modules.SessionModule.DTO;

namespace SmartEdu.Modules.SessionModule.Converters
{
    /// <summary>
    /// Json converter for tokens data
    /// </summary>
    public class CreateSessionJsonConverter : JsonConverter<CreateSessionDTO>
    {
        public override CreateSessionDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return null;
        }

        public override void Write(Utf8JsonWriter writer, CreateSessionDTO tokensData, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("RefreshToken", tokensData.refreshToken);
            writer.WriteString("AccessToken", tokensData.accessToken);

            writer.WriteEndObject();
        }
    }
}
