namespace SmartEdu.Modules.CourseModule.DTO
{
    public record CreateAnswerFieldDTO(int? courseId, int? courseExerciseId, int? exercisePageId, string? coords);
}
