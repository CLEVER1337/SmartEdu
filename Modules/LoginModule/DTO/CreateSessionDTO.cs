namespace SmartEdu.Modules.LoginModule.Core
{
    /// <summary>
    /// Data from login request
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    public record CreateSessionDTO(string? login, string? password);
}
