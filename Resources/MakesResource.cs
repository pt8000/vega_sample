
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VegaApp.Resources
{
    public class MakesResource : KeyValuePairResource
    {
        //normalnie kazdy w oddzielnej klasie powinien byc
        // ctrl + . w menu jest opcja przenies do oddzielnej klasy, dobry tool
       
        public ICollection<KeyValuePairResource> Modele { get; set; }

        public MakesResource()
        {
            Modele = new Collection<KeyValuePairResource>(); //w collection nie ma dostepu do obiektow przez indeks, tak jak w liscie
        }
    }
}