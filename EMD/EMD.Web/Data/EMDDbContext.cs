using EMD.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EMD.Web.Data
{
    public class EMDDbContext : DbContext
    {
        public EMDDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Emd> Emds { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}
