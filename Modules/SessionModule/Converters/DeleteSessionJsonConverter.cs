using SmartEdu.Modules.SessionModule.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartEdu.Modules.SessionModule.Converters
{
    public class DeleteSessionJsonConverter : JsonConverter<DeleteSessionDTO>
    {
        public override DeleteSessionDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                return new DeleteSessionDTO(accessToken);
            else
                return null;
        }

        public override void Write(Utf8JsonWriter writer, DeleteSessionDTO tokensData, JsonSerializerOptions options)
        {
        }
    }
}
