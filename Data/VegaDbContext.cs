using Microsoft.EntityFrameworkCore;
using VegaApp.Core.Models;

namespace VegaApp.Data
{
    public class VegaDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Make> Makes { get; set; } //modele i inne zrobia sie same ze względu na powiązania z Makes
        public DbSet<Model> Models { get; set; } //ale zeby uzywac modeli w _context w controllerach musimy i tak dodac dbset
        public DbSet<Feature> Feature { get; set; }

        public VegaDbContext(DbContextOptions<VegaDbContext> options) : base(options)
        {            
        }

        // robimy composite key dla VehicleFeature z fluidapi        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(vf => 
                new { vf.VehicleId, vf.FeatureId });
        }
    }
}