using Microsoft.EntityFrameworkCore;
using SmartEdu.Modules.UserModule.Core;
using System.Runtime.CompilerServices;

namespace SmartEdu
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(string connectionString) 
        {
            _connectionString = connectionString;
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        private string _connectionString;

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Tutor> Tutors { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<UserData> UsersData { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString,
                ServerVersion.AutoDetect(_connectionString));
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
