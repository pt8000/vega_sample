using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace VegaApp.Core.Models
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
}