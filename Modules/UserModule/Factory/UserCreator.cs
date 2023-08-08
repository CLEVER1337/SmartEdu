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
        public async Task RegisterUser(string login, string salt, string hashedPassword)
        {
            var user = CreateUser(login, salt, hashedPassword);

            await User.Save(user);

            using(var context = new ApplicationContext())
            {
                user = await GetUser(login);
                context.Update(user!);
                user!.UserData.UserId = user.Id;
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

        /// <summary>
        /// Return user from db by id
        /// DON'T FORGET TO UPDATE DATA
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<User?> GetUser(int id)
        {
            User? user;

            using (var context = new ApplicationContext())
            {
                user = await context.Users.Include(u => u.UserData).FirstOrDefaultAsync(u => u.Id == id);
            }

            return user;
        }
    }
}
