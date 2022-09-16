using AccountService.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Database
{
    public class CustomerIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public CustomerIdentityContext()
        {

        }

        public CustomerIdentityContext(DbContextOptions<CustomerIdentityContext> options) : base(options)
        {

        }

        public virtual DbSet<Contact> Contact { get; set; } = null!;
        public virtual DbSet<Profile> Profile { get; set; } = null!;
        public virtual DbSet<ServiceRequest> ServiceRequest { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>()
               .HasData(
               new ApplicationUser()
               {
                   UserName = "Sam",
                   PasswordHash = "123456",
                   Email = "sam@gmail.com",
                   NormalizedEmail = "SAM@GMAIL.COM",
                   UserRole = 0
               });
        }
    }
}
