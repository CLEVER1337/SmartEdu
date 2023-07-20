using SmartEdu.Modules.CourseModule.DecoratorElements;

namespace SmartEdu.Modules.CourseModule.Core
{
    public class CoursePage : CoursePageElement
    {
        public CoursePage() 
        {
            elements = new List<CoursePageElement>();
        }

        public List<CoursePageElement> elements;

        public string? TemplateName;
    }
}
