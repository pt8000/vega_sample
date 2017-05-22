using System.ComponentModel.DataAnnotations.Schema;

namespace VegaApp.Core.Models
{
    [Table("VehicleFeatures")]
    public class VehicleFeature
    {
        // dwa id nizej beda composite primary key, zeby nie robic oddzielnego id bo i tak te dwa id beda zawsze unikalne
        public int VehicleId { get; set; }
        public int FeatureId { get; set; }
        public Vehicle Vehicle { get; set; }
        public Feature Feature { get; set; }
    }
}