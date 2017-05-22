using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VegaApp.Core.Models
{
    [Table("Models")]
    public class Model
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public Make Make { get; set; } //referencja wsteczna do tego ze ten model jest na liscie modeli w Make
        public int MakeId { get; set; } //zeby nie dodawac do modelu calego obiektu Make, warto miec tylko jego id. nazwa musi byc: klasa plus pole z kluczem glownym

    }
}