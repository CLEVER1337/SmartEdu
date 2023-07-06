using SmartEdu.Modules.CourseModule.DecoratorElements;

namespace SmartEdu.Modules.CourseModule.Core
{
    public class CoursePage : CoursePageElement
    {
        public Course OwningCourse { get; set; }

        private List<CoursePageElement> elements;
    }
}
