using Microsoft.EntityFrameworkCore;
using BaiOnline3.Models;
using BCrypt.Net;

namespace BaiOnline3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Intern> Interns { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<AllowAccess> AllowAccesses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "EnterpriseUser" }
            );


            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    RoleId = 1,
                    FullName = "Admin User",
                    DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    UserId = 2,
                    Username = "Nick",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Nick1234"),
                    RoleId = 2,
                    FullName = "Nicky",
                    DateOfBirth = new DateTime(1995, 5, 10, 0, 0, 0, DateTimeKind.Utc)
                }
            );
            modelBuilder.Entity<AllowAccess>().HasData(
                new AllowAccess
                {
                    Id = 1,
                    RoleId = 1,
                    TableName = "Intern",
                    AccessProperties = "Id,InternName,InternAddress,ImageData,DateOfBirth,InternMail,InternMailReplace,University,CitizenIdentification,CitizenIdentificationDate,Major,Internable,FullTime,Cvfile,InternSpecialized,TelephoneNum,InternStatus,RegisteredDate,HowToKnowAlta,InternPassword,ForeignLanguage,YearOfExperiences,PasswordStatus,ReadyToWork,InternEnabled,EntranceTest,Introduction,Note,LinkProduct,JobFields,HiddenToEnterprise"
                },
                new AllowAccess
                {
                    Id = 2,
                    RoleId = 2,
                    TableName = "Intern",
                    AccessProperties = "InternMail,InternName,Major"
                }
            );


            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);
        }
    }
}