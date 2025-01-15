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
        public DbSet<Department> Department { get; set; } = null!;
        public DbSet<UserDepartment> UserDepartment { get; set; } = null!;
        public DbSet<Models.Ticket> Ticket { get; set; } = null!;
        public DbSet<MainProblem> MainProblems { get; set; } = null!;
        public DbSet<SubProblem> SubProblems { get; set; } = null!;
        public DbSet<Replies> Replies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning));
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(s => s.UserDepartments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            builder.Entity<User>()
                .HasMany(s => s.UserTickets)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.CustomerId);

            builder.Entity<User>()
                .HasMany(s => s.UserTickets)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.AssignorId);

            builder.Entity<User>()
                .HasMany(s => s.UserTickets)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.CustomerId);

            builder.Entity<User>()
                .HasMany(s => s.UserTickets)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.AssignId);

            builder.Entity<Department>()
                .HasMany(s => s.UserDepartments)
                .WithOne(c => c.Department)
                .HasForeignKey(c => c.UserId);

            builder.Entity<Department>()
                .HasMany(s => s.DepartmentTickets)
                .WithOne(c => c.department)
                .HasForeignKey(c => c.DepartmentId);

            builder.Entity<UserDepartment>()
                .HasKey(ud => new { ud.UserId, ud.DepartmentId });

            builder.Entity<UserDepartment>()
                .HasOne(ud => ud.User)
                .WithMany(u => u.UserDepartments)
                .HasForeignKey(ud => ud.UserId);

            builder.Entity<UserDepartment>()
                .HasOne(ud => ud.Department)
                .WithMany(d => d.UserDepartments)
                .HasForeignKey(ud => ud.DepartmentId);

            builder.Entity<Models.Ticket>()
                .Property(o => o.Status)
                .HasConversion<string>();

            builder.Entity<Department>()
                .HasMany(s => s.MainProblems)
                .WithOne(x => x.Department)
                .HasForeignKey(ud => ud.DepartmentId);

            builder.Entity<MainProblem>()
                .HasMany(s => s.SubProblems)
                .WithOne(x => x.MainProblem)
                .HasForeignKey(ud => ud.MainProblemId);
            builder.Entity<Models.Ticket>()
                .HasMany(s => s.Replies)
                .WithOne(x => x.Ticket)
                .HasForeignKey(ud => ud.TicketId);

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