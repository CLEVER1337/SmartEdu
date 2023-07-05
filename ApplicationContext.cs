using Microsoft.EntityFrameworkCore;
using SmartEdu.Modules.CourseModule.Core;
using SmartEdu.Modules.CourseModule.DecoratorElements;
using SmartEdu.Modules.UserModule.Core;

namespace SmartEdu
{
    /// <summary>
    /// EFCore db context
    /// </summary>
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() 
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public static string? connectionString;

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Tutor> Tutors { get; set; } = null!;

        public DbSet<Student> Students { get; set; } = null!;

        public DbSet<UserData> UsersData { get; set; } = null!;

        public DbSet<Course> Courses { get; set; } = null!;

        public DbSet<CoursePageElement> CoursePageElements { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>().ToTable("usersdata");
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserData).WithOne(ud => ud.User)
                .HasForeignKey<User>(u => u.UserDataId);
        }
    }
}
