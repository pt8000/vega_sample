using AutoMapper;
using System.Linq;
using System.Collections.Generic;

using VegaApp.Core.Models;
using VegaApp.Resources;

namespace VegaApp.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //----------------------
            // domain to api resources, z reversemap od razu w obie strony
            //----------------------            
            CreateMap<Make, MakesResource>().ReverseMap(); //robi obie mapy, z makes to makesview i odwrotna też
            CreateMap<Make, KeyValuePairResource>().ReverseMap();
            CreateMap<Model, KeyValuePairResource>().ReverseMap();
            CreateMap<Feature, KeyValuePairResource>().ReverseMap();

            //save & update
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            //show full vehicle data
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make ))
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource { Id = vf.Feature.Id, Name = vf.Feature.Name })));
            
            //----------------------
            //api resource to domain
            //----------------------
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vview => vview.Contact.Name))//dlatego ze nie sa takie same nazwy w nich i ksztalty klas sa rozne
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vview => vview.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vview => vview.Contact.Phone))
                //.ForMember(v => v.Features, opt => opt.MapFrom(vview => vview.Features.Select(id => new VehicleFeature { FeatureId = id}))); //z kazdego id ktore przchodzi w tablicy robimy obiekt vehiclefeatures
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) =>
                {
                    //remove unselected features (te sa w Vehiclu zapisanym w bazie, jak ktos odznaczyl w vehicleResource to usun z bazy)
                    /*var removedFeatures = new List<VehicleFeature>();

                    foreach (var f in v.Features)
                        if (!vr.Features.Contains(f.FeatureId))
                            removedFeatures.Add(f);*/
                    
                    //po refaktoringu do linq wyglada to tak
                    var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();
                    foreach (var rf in removedFeatures)
                        v.Features.Remove(rf);

                    //add new features
                    /*foreach (var id in vr.Features)
                        if (!v.Features.Any(f => f.FeatureId == id))
                            v.Features.Add(new VehicleFeature { FeatureId = id });*/

                    //refaktoring
                    var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id });
                    foreach(var f in addedFeatures)
                        v.Features.Add(f);
                });
        }
    }
}
