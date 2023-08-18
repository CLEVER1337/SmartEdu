namespace SmartEdu.Modules.CourseModule.DTO
{
    /// <summary>
    /// Set coords to element
    /// </summary>
    /// <param name="elementId"></param>
    /// <param name="coords"></param>
    public record UpdateCourseElementCoordsDTO(int? elementId, string? coords);
}
