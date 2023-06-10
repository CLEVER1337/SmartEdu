using System.Text.Json;
using System.Text.Json.Serialization;
using SmartEdu.Modules.RegistrationModule.Core;

namespace SmartEdu.Modules.RegistrationModule.Converters
{
    /// <summary>
    /// Json converter for registration data
    /// </summary>
    public class RegistrationJsonConverter : JsonConverter<RegistrationData>
    {
        public override RegistrationData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? login = null;
            string? password = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString()!;
                    reader.Read();
                    switch (propertyName?.ToLower())
                    {
                        case "login":
                            login = reader.GetString()!;
                            break;
                        case "password":
                            password = reader.GetString()!;
                            break;
                    }
                }
            }

            if (login == null || password == null)
                return null;
            else
                return new RegistrationData(login, password);
        }

        public override void Write(Utf8JsonWriter writer, RegistrationData user, JsonSerializerOptions options)
        {
        }
    }
}
