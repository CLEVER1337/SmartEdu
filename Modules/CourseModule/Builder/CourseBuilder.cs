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

        public async void BuiildExercise(string name)
        {
            using (var context = new ApplicationContext())
            {
                context.Courses.Update(_result!);

                _result?.Exercises.Add(new CourseExercise(name));

                await context.SaveChangesAsync();
            }
        }

        public async void BuildElement<T>(int courseExereciseId, int exercisePageId, Coord coords) where T : CourseElement, new()
        {
            using (var context = new ApplicationContext())
            {
                context.Courses.Update(_result!);

                _result?.Exercises[courseExereciseId].Pages[exercisePageId].Elements.Add(new T());

                await context.SaveChangesAsync();
            }
        }

        public async void BuildPage(int courseExereciseId)
        {
            using (var context = new ApplicationContext())
            {
                context.Courses.Update(_result!);

                _result?.Exercises[courseExereciseId].Pages.Add(new CourseExercisePage());

                await context.SaveChangesAsync();
            }
        }

        public async static Task<Course?> GetCourse(int id)
        {
            Course? course;

            using (var context = new ApplicationContext())
            {
                course = await context.Courses.Include(c => c.Author).Include(c => c.Exercises).FirstOrDefaultAsync(c => c.Id == id);
            }

            return course;
        }

        public async static Task<CourseElement?> GetPageElement(int id)
        {
            CourseElement? element;

            using (var context = new ApplicationContext())
            {
                element = await context.CoursePageElements.FirstOrDefaultAsync(c => c.Id == id);
            }

            return element;
        }

        public async static Task<T?> GetPageElement<T>(int id) where T : CourseElement
        {
            T? element;

            using (var context = new ApplicationContext())
            {
                element = (T?)await context.CoursePageElements.FirstOrDefaultAsync(c => c.Id == id);
            }

            return element;
        }
    }
}
