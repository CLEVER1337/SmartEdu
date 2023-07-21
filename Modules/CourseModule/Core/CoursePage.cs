using SmartEdu.Modules.CourseModule.DecoratorElements;

namespace SmartEdu.Modules.CourseModule.Core
{
    public class CoursePage : CoursePageElement
    {
        public CoursePage() 
        {
            Elements = new List<CoursePageElement>();
        }

        public List<CoursePageElement> Elements { get; set; }
    }
}
