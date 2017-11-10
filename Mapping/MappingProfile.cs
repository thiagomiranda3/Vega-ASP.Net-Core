using AutoMapper;
using Vega_ASP.Net_Core.Controllers.Resources;
using Vega_ASP.Net_Core.Core.Models;
using System.Linq;
using System.Collections.Generic;

namespace Vega_ASP.Net_Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource {
                    Name = v.ContactName,
                    Email = v.ContactEmail,
                    Phone = v.ContactPhone
                }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource {
                    Name = v.ContactName,
                    Email = v.ContactEmail,
                    Phone = v.ContactPhone
                }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf =>
                    new KeyValuePairResource
                    {
                        Id = vf.FeatureId,
                        Name = vf.Feature.Name
                    })))
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make));
                    
            // API Resource to Domain
            CreateMap<VehicleQueryResource, VehicleQuery>();
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) => {
                    // Remover features não recebidas
                    var removedFeatures = v.Features
                                           .Where(f => !vr.Features.Contains(f.FeatureId));
                    foreach(var f in removedFeatures)
                        v.Features.Remove(f);

                    // Adicionar features novas
                    var addedFeatures = vr.Features
                                          .Where(id => !v.Features.Any(f => f.FeatureId == id))
                                          .Select(id => new VehicleFeature {FeatureId = id});
                    foreach(var f in addedFeatures)
                        v.Features.Add(f);
                });
        }
    }
}