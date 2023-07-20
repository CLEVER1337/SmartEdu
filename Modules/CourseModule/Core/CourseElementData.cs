namespace SmartEdu.Modules.CourseModule.Core
{
    public record CourseElementData(string? discriminator, int? CourseId, int? coursePageId, string? coords);
}
