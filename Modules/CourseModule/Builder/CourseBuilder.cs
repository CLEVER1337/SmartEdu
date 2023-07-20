using SmartEdu.Modules.CourseModule.DecoratorElements;
using SmartEdu.Modules.CourseModule.Core;
using Microsoft.EntityFrameworkCore;
using SmartEdu.Modules.UserModule.Core;

namespace SmartEdu.Modules.CourseModule.Builder
{
    public class CourseBuilder
    {
        public CourseBuilder(int courseId)
        {
            _result = GetCourse(courseId).Result;
        }

        private Course? _result;

        public Course? Result
        {
            get => _result;
        }

        public async void BuildElement<T>(int coursePageId, Coord coords) where T : CoursePageElement, new()
        {
            using (var context = new ApplicationContext())
            {
                context.Courses.Update(_result!);

                _result?.Pages[coursePageId].elements.Add(new T());

                await context.SaveChangesAsync();
            }
        }

        public async void BuildPage()
        {
            using (var context = new ApplicationContext())
            {
                context.Courses.Update(_result!);

                _result?.Pages.Add(new CoursePage());

                await context.SaveChangesAsync();
            }
        }

        public async static Task<Course?> GetCourse(int id)
        {
            Course? course;

            using (var context = new ApplicationContext())
            {
                course = await context.Courses.Include(c => c.Author).Include(c => c.Pages).FirstOrDefaultAsync(c => c.Id == id);
            }

            return course;
        }
    }
}
