using SmartEdu.Modules.SessionModule.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartEdu.Modules.SessionModule.Converters
{
    public class RefreshTokensJsonConverter : JsonConverter<RefreshTokensDTO>
    {
        public override RefreshTokensDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? accessToken = null;
            string? refreshToken = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString()!;
                    reader.Read();
                    switch (propertyName?.ToLower())
                    {
                        case "AccesToken":
                            accessToken = reader.GetString()!;
                            break;
                        case "RefreshToken":
                            refreshToken = reader.GetString()!;
                            break;
                    }
                }
            }

            if (accessToken != null)
                return new RefreshTokensDTO(refreshToken, accessToken);
            else
                return null;
        }

        public override void Write(Utf8JsonWriter writer, RefreshTokensDTO tokensData, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("RefreshToken", tokensData.refreshToken);
            writer.WriteString("AccessToken", tokensData.accessToken);

            writer.WriteEndObject();
        }
    }
}
