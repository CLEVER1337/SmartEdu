namespace SmartEdu.Modules.CourseModule.DTO
{
    public record CreateCourseElementDTO(string? discriminator, int? courseId, int? courseExerciseId, int? exercisePageId, string? coords);
}
