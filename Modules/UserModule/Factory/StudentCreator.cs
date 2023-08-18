using Microsoft.EntityFrameworkCore;
using SmartEdu.Modules.UserModule.Core;


namespace SmartEdu.Modules.UserModule.Factory
{
    public class StudentCreator : UserCreator
    {
        public StudentCreator() 
        {

        }

        protected override User CreateUser(string login, string salt, string hashedPassword)
        {
            return new Student(login, salt, hashedPassword);
        }

        /// <summary>
        /// Return student from db by login
        /// DON'T FORGET TO UPDATE DATA
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async static Task<Student?> GetStudent(string login)
        {
            Student? student;

            using (var context = new ApplicationContext())
            {
                student = await context.Students.Include(s => s.UserData).Include(s => s.OwningCourses).FirstOrDefaultAsync(s => s.UserData.Login == login);
            }

            return student;
        }

        /// <summary>
        /// Return student from db by id
        /// DON'T FORGET TO UPDATE DATA
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<Student?> GetStudent(int id)
        {
            Student? student;

            using (var context = new ApplicationContext())
            {
                student = await context.Students.Include(s => s.UserData).Include(s => s.OwningCourses).FirstOrDefaultAsync(t => t.Id == id);
            }

            return student;
        }
    }
}
