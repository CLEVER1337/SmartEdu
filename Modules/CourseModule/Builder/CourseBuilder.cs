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

        /// <summary>
        /// Create exercise and add it to course
        /// </summary>
        /// <param name="name"></param>
        public async void BuildExercise(string name)
        {
            using (var context = new ApplicationContext())
            {
                context.Courses.Update(_result!);

                _result?.Exercises.Add(new CourseExercise(name));

                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Create element with any type and add it to course
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="courseExereciseId"></param>
        /// <param name="exercisePageId"></param>
        /// <param name="coords"></param>
        public async void BuildElement<T>(int courseExereciseId, int exercisePageId, Coord coords) where T : CourseElement, new()
        {
            using (var context = new ApplicationContext())
            {
                context.Courses.Update(_result!);

                var element = new T();

                element.Coords = coords.GetCoords();

                _result?.Exercises[courseExereciseId].Pages[exercisePageId].Elements.Add(element);

                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Create page and add it to course
        /// </summary>
        /// <param name="courseExereciseId"></param>
        public async void BuildPage(int courseExereciseId)
        {
            using (var context = new ApplicationContext())
            {
                context.Courses.Update(_result!);

                _result?.Exercises[courseExereciseId].Pages.Add(new CourseExercisePage());

                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get course from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<Course?> GetCourse(int id)
        {
            Course? course;

            using (var context = new ApplicationContext())
            {
                course = await context.Courses.Include(c => c.Author).Include(c => c.Exercises).FirstOrDefaultAsync(c => c.Id == id);
            }

            return course;
        }

        /// <summary>
        /// Get exercise from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<CourseExercise?> GetExercise(int id)
        {
            CourseExercise? exercise;

            using(var context = new ApplicationContext())
            {
                exercise = await context.CourseExercises.Include(e => e.Pages).FirstOrDefaultAsync(e => e.Id == id);
            }

            return exercise;
        }

        /// <summary>
        /// Get element of page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<CourseElement?> GetPageElement(int id)
        {
            CourseElement? element;

            using (var context = new ApplicationContext())
            {
                element = await context.CoursePageElements.FirstOrDefaultAsync(c => c.Id == id);
            }

            return element;
        }

        /// <summary>
        /// Get element of page with any type besides page
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
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
