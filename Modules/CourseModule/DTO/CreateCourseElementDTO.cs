namespace SmartEdu.Modules.CourseModule.DTO
{
    /// <summary>
    /// Create element
    /// </summary>
    /// <param name="discriminator"></param>
    /// <param name="courseId"></param>
    /// <param name="courseExerciseId"></param>
    /// <param name="exercisePageId"></param>
    /// <param name="coords"></param>
    public record CreateCourseElementDTO(string? discriminator, int? courseId, int? courseExerciseId, int? exercisePageId, string? coords);
}
