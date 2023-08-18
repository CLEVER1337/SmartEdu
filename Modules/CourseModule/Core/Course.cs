using SmartEdu.Modules.UserModule.Core;

namespace SmartEdu.Modules.CourseModule.Core
{
    public class Course : BaseEntity
    {
        public Course(string name) 
        {
            Name = name;

            Exercises = new List<CourseExercise>();
        }

        public List<CourseExercise> Exercises { get; set; }

        public Tutor? Author { get; set; }

        public int AuthorId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Save course in db
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async static Task Save(Course course)
        {
            using (var context = new ApplicationContext())
            {
                await context.Courses.AddAsync(course);
                await context.SaveChangesAsync();
            }
        }
    }
}
