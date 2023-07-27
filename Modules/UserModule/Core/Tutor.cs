using SmartEdu.Modules.CourseModule.Core;

namespace SmartEdu.Modules.UserModule.Core
{
    public class Tutor : User
    {
        public Tutor() 
        {
            OwnedCourses = new List<CourseExercise>();
        }

        public Tutor(string login, string salt, string hashedPassword) : base(login, salt, hashedPassword)
        {
            OwnedCourses = new List<CourseExercise>();
        }

        public List<CourseExercise> OwnedCourses { get; set; }
    }
}
