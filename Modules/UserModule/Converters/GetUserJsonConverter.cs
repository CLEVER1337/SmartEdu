using SmartEdu.Modules.CourseModule.DTO;
using SmartEdu.Modules.UserModule.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartEdu.Modules.UserModule.Converters
{
    public class GetUserJsonConverter : JsonConverter<GetUserDTO>
    {
        public override GetUserDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString()!;
                    reader.Read();
                    switch (propertyName?.ToLower())
                    {
                    }
                }
            }

            return new GetUserDTO();
        }

        public override void Write(Utf8JsonWriter writer, GetUserDTO registrationData, JsonSerializerOptions options)
        {
        }
    }
}
