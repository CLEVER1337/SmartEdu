using SmartEdu.Modules.UserModule.Core;
using SmartEdu.Modules.UserModule.Factory;

namespace SmartEdu.Modules.RegistrationModule.Ports
{
    public interface IRegistrationService
    {
        /// <summary>
        /// Register user if login isn't already in use and add him in db
        /// </summary>
        /// <param name="login"></param>
        /// <param name="salt"></param>
        /// <param name="hashedPassword"></param>
        /// <param name="userCreator"></param>
        /// <returns></returns>
        public User? Register(string login, string salt, string hashedPassword, UserCreator userCreator);
    }
}
