﻿namespace SmartEdu.Modules.RegistrationModule.Core
{
    /// <summary>
    /// Data from registration request
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    public record RegistrationData(string? login, string? password);
}
