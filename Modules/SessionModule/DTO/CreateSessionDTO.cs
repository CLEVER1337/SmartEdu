namespace SmartEdu.Modules.SessionModule.DTO
{
    /// <summary>
    /// Tokens
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <param name="accessToken"></param>
    public record CreateSessionDTO(string? refreshToken, string? accessToken);
}
