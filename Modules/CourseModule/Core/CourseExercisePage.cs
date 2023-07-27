using SmartEdu.Modules.CourseModule.DecoratorElements;

namespace SmartEdu.Modules.CourseModule.Core
{
    public class CourseExercisePage : CourseElement
    {
        public CourseExercisePage() 
        {
            Elements = new List<CourseElement>();
        }

        public List<CourseElement> Elements { get; set; }
    }
}
