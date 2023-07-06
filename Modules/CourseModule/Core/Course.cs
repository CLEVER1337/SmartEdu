using SmartEdu.Modules.UserModule.Core;

namespace SmartEdu.Modules.CourseModule.Core
{
    public class Course : BaseEntity
    {
        public Course(string name) 
        {
            Name = name;
        }

        private List<CoursePage> pages;

        public Tutor? Author { get; set; }

        public int AuthorId { get; set; }

        public string Name { get; set; }

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
