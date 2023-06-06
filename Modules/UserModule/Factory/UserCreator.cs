using SmartEdu.Modules.UserModule.Core;

namespace SmartEdu.Modules.UserModule.Factory
{
    public abstract class UserCreator
    {
        public UserCreator() 
        {

        }

        public abstract User CreateUser();
    }
}
