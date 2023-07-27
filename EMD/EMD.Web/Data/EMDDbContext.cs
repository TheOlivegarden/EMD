using EMD.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EMD.Web.Data
{
    public class EMDDbContext : DbContext
    {
        public EMDDbContext(DbContextOptions<EMDDbContext> options) : base(options)
        {
        }
        public DbSet<Emd> Emds { get; set; }

        public DbSet<Tasks> Tasks { get; set; }
    }
}
