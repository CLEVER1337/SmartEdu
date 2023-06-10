using SmartEdu.Modules.UserModule.Core;

namespace SmartEdu.Modules.UserModule.Factory
{
    public class TutorCreator : UserCreator
    {
        public TutorCreator() 
        {

        }

        protected override User CreateUser(string login, string salt, string hashedPassword)
        {
            return new Tutor(login, salt, hashedPassword);
        }
    }
}
