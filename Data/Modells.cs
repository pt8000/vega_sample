using System.Collections.Generic;

namespace VegaApp.Modells
{
    public class Makes    
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Model> Modele { get; set; }
    }

    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Features> Features { get; set; }
    }

    public class Features
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}