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

        public async static Task<Tutor?> GetTutor(string login)
        {
            Tutor? tutor;

            using (var context = new ApplicationContext())
            {
                tutor = await context.Tutors.Include(t => t.UserData).Include(t => t.OwnedCourses).FirstOrDefaultAsync(t => t.UserData.Login == login);
            }

            return tutor;
        }

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
