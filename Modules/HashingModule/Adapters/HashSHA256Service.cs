using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SmartEdu.Modules.HashingModule.Ports;

namespace SmartEdu.Modules.HashingModule.Adapters
{
    public class HashSHA256Service : IHashService
    {
        private string _salt = null!;
        private string _hash = null!;

        public string Salt 
        { 
            get { return _salt; }
        }

        public string Hash
        {
            get 
            { 
                return _hash;
            }
            set 
            { 
                HashFunction(value);
            }
        }

        public void HashFunction(string data) 
        {
            // generate a 128-bit salt using a sequence of cryptographically strong random bytes.
            _salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(128 / 8));

            // get hash
            _hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(data,
                Encoding.ASCII.GetBytes(_salt),
                KeyDerivationPrf.HMACSHA256,
                100000,
                256 / 8));
        }

        public void HashFunction(string data, string salt)
        {
            _salt = salt;

            // get hash
            _hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(data,
                Encoding.ASCII.GetBytes(_salt),
                KeyDerivationPrf.HMACSHA256,
                100000,
                256 / 8));
        }
    }
}
