namespace VegaApp.Resources
{

    /*public class ModelResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //zwrotne odniesienie usuwamy stad bo tworzy petle pomiedz Makes i Models
    }*/

    //jedna klasa zastapila nam wiele klas takich samych, np. wyzej modelresource, featureresource itp
    public class KeyValuePairResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}