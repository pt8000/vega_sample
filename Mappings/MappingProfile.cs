using AutoMapper;
using VegaApp.Modells;
using VegaApp.Resources;

namespace VegaApp.Mappers
{
    public class MappingProfile : Profile {
        public MappingProfile() {
            // Add as many of these lines as you need to map your objects
            CreateMap<Make, MakesView>().ReverseMap(); //robi obie mapy, z makes to makesview i odwrotna też
            CreateMap<Model, ModelView>().ReverseMap();
        }
    }
}
