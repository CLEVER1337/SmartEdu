namespace SmartEdu.Modules.CourseModule.Core
{
    public record CourseElementData(string? discriminator, int? courseId, int? coursePageId, int? elementId, string? coords, string? value);
}
