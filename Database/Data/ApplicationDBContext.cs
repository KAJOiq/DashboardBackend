using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Ticket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ticket.Data
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Department> Department {get; set;} = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning));
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            List<IdentityRole> roles =
            [
                new()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new() 
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            ];
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}