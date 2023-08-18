using SmartEdu.Modules.CourseModule.Core;

namespace SmartEdu.Modules.UserModule.Core
{
    public class Student : User
    {
        public Student() 
        {
            OwningCourses = new List<Course>();
        }

        public Student(string login, string salt, string hashedPassword) : base(login, salt, hashedPassword) 
        {
            OwningCourses = new List<Course>();
        }

        public List<Course> OwningCourses { get; set; }
    }
}
