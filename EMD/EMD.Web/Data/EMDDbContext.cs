using EMD.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EMD.Web.Data
{
    public class EMDDbContext : DbContext
    {
        public EMDDbContext(DbContextOptions<EMDDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>()
            .HasMany(e => e.Tasks)
            .WithMany(e => e.Employees);
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Tasks> Tasks { get; set; }
    }
}
