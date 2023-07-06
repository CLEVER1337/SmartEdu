using SmartEdu.Modules.CourseModule.Core;

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

        public List<Course> OwnedCourses { get; set; }
    }
}
