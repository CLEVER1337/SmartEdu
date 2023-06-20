namespace SmartEdu.Modules.SessionModule.Core
{
    /// <summary>
    /// Tokens
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <param name="accessToken"></param>
    public record TokensData(string? refreshToken, string? accessToken);
}
