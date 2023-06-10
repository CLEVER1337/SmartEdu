using SmartEdu.Modules.UserModule.Core;


namespace SmartEdu.Modules.UserModule.Factory
{
    public class StudentCreator : UserCreator
    {
        public StudentCreator() 
        {

        }

        protected override User CreateUser(string login, string salt, string hashedPassword)
        {
            return new Student(login, salt, hashedPassword);
        }
    }
}
