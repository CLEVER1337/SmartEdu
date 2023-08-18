namespace SmartEdu.Modules.CourseModule.DTO
{
    /// <summary>
    /// Set image's path, text
    /// </summary>
    /// <param name="elementId"></param>
    /// <param name="value"></param>
    public record UpdateCourseElementValueDTO(int? elementId, string? value);
}
