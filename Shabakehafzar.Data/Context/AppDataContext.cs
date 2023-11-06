using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shabakehafzar.Core.Models;
using System.Data;

namespace Shabakehafzar.Data.Context
{
    public class AppDataContext : IdentityDbContext<IdentityUser, AppRole, string> , IAppDataContext
    {
        public AppDataContext(DbContextOptions options)
                : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonWithOwnedEntityType> PersonWithOwnedEntityType { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Seed Data

            var HashPassword = new PasswordHasher<User>();

            var defultadmin = new User()
            {
                FirstName = "Admin",
                LastName = "Admin",
                Email = "testAdmin@test.com",
                UserName = "Admin",
                NormalizedUserName = "Admin",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                
            };
            defultadmin.PasswordHash = HashPassword.HashPassword(defultadmin, "123456Admin#");

            var defultuser = new User()
            {
                FirstName = "User",
                LastName = "User",
                Email = "testUser@test.com",
                UserName = "User",
                NormalizedUserName = "User",
                PhoneNumber = "+222222222222",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

            };
            defultuser.PasswordHash = HashPassword.HashPassword(defultadmin, "123456User#"); ;

            modelBuilder.Entity<User>().HasData(defultadmin, defultuser);

            string[] roles = new string[] { "admin_role", "user_role" };
            var adminRole = new AppRole(roles[0])
            {
                Id = Guid.NewGuid().ToString(),
                NormalizedName = roles[0].ToUpper(),
                CreatedDate = DateTime.UtcNow
            };

            var userRole = new AppRole(roles[1])
            {
                Id = Guid.NewGuid().ToString(),
                NormalizedName = roles[1].ToUpper(),
                CreatedDate = DateTime.UtcNow
            };

            modelBuilder.Entity<AppRole>().HasData(adminRole, userRole);

            var assignDefultAdminToAdminRole = new IdentityUserRole<string>
            {
                UserId = defultadmin.Id,
                RoleId = adminRole.Id
            };

            var assignDefultUserToUserRole = new IdentityUserRole<string>
            {
                UserId = defultuser.Id,
                RoleId = userRole.Id
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(assignDefultAdminToAdminRole, assignDefultUserToUserRole);

            #endregion


            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<AppRole>().ToTable("AppRole");
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Address>().ToTable("Address");

            modelBuilder.Entity<Address>()
                .HasOne(c => c.Person)
                .WithMany(p => p.Addresses)
                .HasForeignKey(c => c.PersonId);

            modelBuilder.Entity<PersonWithOwnedEntityType>().OwnsMany(p => p.Addresses, a =>
            {
                a.WithOwner().HasForeignKey("OwnerId");
                a.Property<Guid>("Id");
                a.HasKey("Id");
            });
        }
    }
}
