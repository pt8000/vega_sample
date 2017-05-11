using Microsoft.EntityFrameworkCore;
using VegaApp.Modells;

namespace VegaApp.Data
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options) : base(options)
        {            
        }

        public DbSet<Make> Makes { get; set; } //modele i inne zrobia sie same ze względu na powiązania z Makes
        public DbSet<Feature> Feature { get; set; }
        
    }
}