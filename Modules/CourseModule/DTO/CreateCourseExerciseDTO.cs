namespace SmartEdu.Modules.CourseModule.DTO
{
    /// <summary>
    /// Create exercise
    /// </summary>
    /// <param name="name"></param>
    /// <param name="courseId"></param>
    public record CreateCourseExerciseDTO(string? name, int? courseId);
}
