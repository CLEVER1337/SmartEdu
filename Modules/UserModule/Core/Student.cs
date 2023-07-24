namespace SmartEdu.Modules.UserModule.Core
{
    public class Student : User
    {
        public Student() 
        {

        }

        public Student(string login, string salt, string hashedPassword) : base(login, salt, hashedPassword) 
        {

        }
    }
}
