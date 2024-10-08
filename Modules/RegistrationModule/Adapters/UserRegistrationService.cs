﻿using Microsoft.EntityFrameworkCore;
using SmartEdu.Modules.RegistrationModule.Ports;
using SmartEdu.Modules.UserModule.Core;
using SmartEdu.Modules.UserModule.Factory;

namespace SmartEdu.Modules.RegistrationModule.Adapters
{
    public class UserRegistrationService : IRegistrationService
    {
        public async Task<User?> Register(string login, string salt, string hashedPassword, UserCreator userCreator)
        {
            using(var context = new ApplicationContext())
            {
                // Check is this login already in db
                var user = await UserCreator.GetUser(login);

                // If login isn't in use then create user and add him in db
                if (user == null)
                {
                    await userCreator.RegisterUser(login, salt, hashedPassword);
                    // Return created user
                    return await UserCreator.GetUser(login);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
