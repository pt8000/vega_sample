using Microsoft.EntityFrameworkCore;
using VegaApp.Modells;

namespace VegaApp.Data
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options) : base(options)
        {            
        }

        public DbSet<Makes> Makes { get; set; }
    }
}