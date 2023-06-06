using SmartEdu.Modules.UserModule.Core;

namespace SmartEdu.Modules.UserModule.Factory
{
    public class StudentCreator : UserCreator
    {
        public StudentCreator() 
        {

        }

        public override User CreateUser()
        {
            return new Student();
        }
    }
}
