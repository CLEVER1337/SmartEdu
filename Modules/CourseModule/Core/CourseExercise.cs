using SmartEdu.Modules.UserModule.Core;

namespace SmartEdu.Modules.CourseModule.Core
{
    /// <summary>
    /// Part of course such as a presentation
    /// </summary>
    public class CourseExercise : BaseEntity
    {
        public CourseExercise(string name) 
        {
            Pages = new List<CourseExercisePage>();
            Name = name;
        }

        public List<CourseExercisePage> Pages { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Save exercise in db
        /// MAY BE DELETED
        /// </summary>
        /// <param name="exercise"></param>
        /// <returns></returns>
        public async static Task Save(CourseExercise exercise)
        {
            using (var context = new ApplicationContext())
            {
                await context.CourseExercises.AddAsync(exercise);
                await context.SaveChangesAsync();
            }
        }
    }
}
