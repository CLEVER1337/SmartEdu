namespace SmartEdu.Modules.UserModule.Core
{
    public class Tutor : User
    {
        public Tutor() 
        {

        }

        public Tutor(string login, string salt, string hashedPassword) : base(login, salt, hashedPassword)
        {

        }
    }
}
