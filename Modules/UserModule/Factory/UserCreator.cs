using Microsoft.EntityFrameworkCore;
using SmartEdu.Modules.UserModule.Core;

namespace SmartEdu.Modules.UserModule.Factory
{
    public abstract class UserCreator
    {
        public UserCreator() 
        {

        }

        protected abstract User CreateUser(string login, string salt, string hashedPassword);

        /// <summary>
        /// Create user and save him in db
        /// </summary>
        /// <param name="login"></param>
        /// <param name="salt"></param>
        /// <param name="hashedPassword"></param>
        public async void RegisterUser(string login, string salt, string hashedPassword)
        {
            var user = CreateUser(login, salt, hashedPassword);

            SaveUser(user);

            using(var context = new ApplicationContext())
            {
                (await GetUser(login))!.UserData.UserId = user.Id;
                //context.Users.Include(u => u.UserData).ToList().FirstOrDefault(u => u.UserData.Login == user.UserData.Login)!.UserData.UserId = user.Id;
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Save user in db
        /// </summary>
        /// <param name="user"></param>
        public async static void SaveUser(User user)
        {
            using(var context = new ApplicationContext())
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Return user from db by login
        /// DON'T FORGET TO UPDATE DATA
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async static Task<User?> GetUser(string login)
        {
            User? user;

            using (var context = new ApplicationContext())
            {
                user = await context.Users.Include(u => u.UserData).FirstOrDefaultAsync(u => u.UserData.Login == login);
            }

            return user;
        }
    }
}
