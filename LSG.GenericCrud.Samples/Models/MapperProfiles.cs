using AutoMapper;
using LSG.GenericCrud.Samples.Models.DTOs;
using LSG.GenericCrud.Samples.Models.Entities;

namespace LSG.GenericCrud.Models
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<ItemDto, Item>()
                .ForMember(dst => dst.JsonMetadata, opts => opts.MapFrom(m => new { m.Color, m.Price, m.Name}));
        }
    }
}