using Microsoft.EntityFrameworkCore;
using SmartEdu.Modules.UserModule.Core;

namespace SmartEdu.Modules.UserModule.Factory
{
    public class TutorCreator : UserCreator
    {
        public TutorCreator() 
        {

        }

        protected override User CreateUser(string login, string salt, string hashedPassword)
        {
            return new Tutor(login, salt, hashedPassword);
        }

        /// <summary>
        /// Return tutor from db by login
        /// DON'T FORGET TO UPDATE DATA
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async static Task<Tutor?> GetTutor(string login)
        {
            Tutor? tutor;

            using (var context = new ApplicationContext())
            {
                tutor = await context.Tutors.Include(t => t.UserData).Include(t => t.OwnedCourses).FirstOrDefaultAsync(t => t.UserData.Login == login);
            }

            return tutor;
        }

        /// <summary>
        /// Return tutor from db by id
        /// DON'T FORGET TO UPDATE DATA
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<Tutor?> GetTutor(int id)
        {
            Tutor? tutor;

            using (var context = new ApplicationContext())
            {
                tutor = await context.Tutors.Include(t => t.UserData).Include(t => t.OwnedCourses).FirstOrDefaultAsync(t => t.Id == id);
            }

            return tutor;
        }
    }
}
