using Microsoft.EntityFrameworkCore;

namespace Sportzall.Models
{
    public class SportzalDBContext:DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        public SportzalDBContext(DbContextOptions<SportzalDBContext> options)
           : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";
            string adminName = "Admin";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Name = adminName, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
