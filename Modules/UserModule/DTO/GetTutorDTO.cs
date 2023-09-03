namespace SmartEdu.Modules.UserModule.DTO
{
    /// <summary>
    /// Get user on client
    /// </summary>
    /// <param name="login"></param>
    public record GetTutorDTO(string? login, GetTutorDTOCoursePreview[] CoursePreviews);

    public record GetTutorDTOCoursePreview(int? cost, int? rating, int? difficulty);
}
