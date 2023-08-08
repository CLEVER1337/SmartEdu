using SmartEdu.Modules.CourseModule.Core;

namespace SmartEdu.Modules.UserModule.Core
{
    public class Tutor : User
    {
        public Tutor() 
        {
            OwnedCourses = new List<Course>();
        }

        public Tutor(string login, string salt, string hashedPassword) : base(login, salt, hashedPassword)
        {
            OwnedCourses = new List<Course>();
        }

        public List<Course> OwnedCourses { get; set; }
    }
}
