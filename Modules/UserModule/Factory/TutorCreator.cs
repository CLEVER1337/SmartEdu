using SmartEdu.Modules.UserModule.Core;

namespace SmartEdu.Modules.UserModule.Factory
{
    public class TutorCreator : UserCreator
    {
        public TutorCreator() 
        {

        }

        public override User CreateUser()
        {
            return new Tutor();
        }
    }
}
