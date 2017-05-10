using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VegaApp.Modells
{
    public class Make  
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public ICollection<Model> Modele { get; set; }

        //inicjacja klasy zeby od razu byla colekcja modeli, nie trzeba inicjiwac juz nigdzie w kodzie, brak tez nullreference error w takim przypadku bo zawsze jest, conajwyzej pusta
        public Make()
        {
            Modele = new Collection<Model>(); //w collection nie ma dostepu do obiektow przez indeks, tak jak w liscie
        }
    }

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

    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}