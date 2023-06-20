using System.Text.Json;
using System.Text.Json.Serialization;
using SmartEdu.Modules.SessionModule.Core;

namespace SmartEdu.Modules.SessionModule.Converters
{
    public class TokensJsonConverter : JsonConverter<TokensData>
    {
        public override TokensData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? accessToken = null;

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
                    }
                }
            }

            if (accessToken != null)
                return new TokensData(null, accessToken);
            else
                return null;
        }

        public override void Write(Utf8JsonWriter writer, TokensData tokensData, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("RefreshTokens", tokensData.refreshToken);
            writer.WriteString("AccessToken", tokensData.accessToken);

            writer.WriteEndObject();
        }
    }
}
