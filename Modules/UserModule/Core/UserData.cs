using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace SmartEdu.Modules.UserModule.Core
{
    public class UserData
    {
        public UserData(string login, string password)
        {
            Login = login;
            Password = password;
        }

        private string _login = null!;
        private byte[] _salt = null!;
        private string _hashedPassword = null!;

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string Password
        {
            get
            {
                return _hashedPassword;
            }
            set
            {
                HashPassword(value);
            }
        }

        private void HashPassword(string password)
        {
            // generate a 128-bit salt using a sequence of cryptographically strong random bytes.
            _salt = RandomNumberGenerator.GetBytes(128 / 8);

            // hash password
            _hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(password,
                _salt,
                KeyDerivationPrf.HMACSHA256,
                100000,
                256 / 8));
        }
    }
}
