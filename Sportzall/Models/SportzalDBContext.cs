using Microsoft.EntityFrameworkCore;

namespace Sportzall.Models
{
    public class SportzalDBContext:DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }
        public DbSet<Week> Week { get; set; }
        public DbSet<Hours> Hours { get; set; }
        public DbSet<Abonement> Abonement { get; set; }
        public DbSet<AbonementsUser> AbonementsUser { get; set; }
        public DbSet<TrenersUser> TrenersUser { get; set; }

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
            string trenerRoleName = "trener";
            string adminName = "Admin";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            Role trenerRole= new Role { Id=3, Name = trenerRoleName };
            User adminUser = new User { Id = 1, Name = adminName, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole,trenerRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
