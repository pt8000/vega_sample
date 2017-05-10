
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VegaApp.Resources
{
    public class MakesView 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelView> Modele { get; set; }

        public MakesView()
        {
            Modele = new Collection<ModelView>(); //w collection nie ma dostepu do obiektow przez indeks, tak jak w liscie
        }
    }

    public class ModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //zwrotne odniesienie usuwamy stad bo tworzy petle pomiedz Makes i Models
    }
}