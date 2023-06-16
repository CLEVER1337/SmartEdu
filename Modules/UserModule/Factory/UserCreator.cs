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
        public void RegisterUser(string login, string salt, string hashedPassword)
        {
            var user = CreateUser(login, salt, hashedPassword);

            SaveUser(user);
        }

        /// <summary>
        /// Save user in db
        /// </summary>
        /// <param name="user"></param>
        public static void SaveUser(User user)
        {
            using(var context = new ApplicationContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            using(var context = new ApplicationContext())
            {
                context.Users.Include(u => u.UserData).ToList().FirstOrDefault(u => u.UserData.Login == user.UserData.Login)!.UserData.UserId = user.Id;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Return user from db by login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static User? GetUser(string login)
        {
            User user;

            using (var context = new ApplicationContext())
            {
                user = context.Users.Include(u => u.UserData).ToList().FirstOrDefault(u => u.UserData.Login == login)!;
            }

            return user;
        }
    }
}
