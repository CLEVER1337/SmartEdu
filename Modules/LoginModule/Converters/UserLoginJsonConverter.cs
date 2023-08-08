using System.Text.Json;
using System.Text.Json.Serialization;
using SmartEdu.Modules.LoginModule.Core;
using SmartEdu.Modules.RegistrationModule.Core;

namespace SmartEdu.Modules.LoginModule.Converters
{
    /// <summary>
    /// Json converter for login data
    /// </summary>
    public class UserLoginJsonConverter : JsonConverter<UserLoginDTO>
    {
        public override UserLoginDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                return new UserLoginDTO(login, password);
        }

        public override void Write(Utf8JsonWriter writer, UserLoginDTO user, JsonSerializerOptions options)
        {
        }
    }
}
