using AutoMapper;
using LSG.GenericCrud.Samples.Models.DTOs;
using LSG.GenericCrud.Samples.Models.Entities;
using Newtonsoft.Json;

namespace LSG.GenericCrud.Models
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<ItemDto, Item>()
                .ForMember(dst => dst.JsonMetadata, opts => opts.MapFrom(m => JsonConvert.SerializeObject(new { m.Color, m.Price, m.Name})));
            CreateMap<Item, ItemDto>()
                .ForMember("Color", cfg => { cfg.MapFrom(m => JsonConvert.DeserializeObject<ItemDto>(m.JsonMetadata).Color);})
                .ForMember("Name", cfg => { cfg.MapFrom(m => JsonConvert.DeserializeObject<ItemDto>(m.JsonMetadata).Name);})
                .ForMember("Price", cfg => { cfg.MapFrom(m => JsonConvert.DeserializeObject<ItemDto>(m.JsonMetadata).Price);});
                // .ForMember(dst => dst.Color, opts => opts.MapFrom(m => ((ItemDto)JsonConvert.DeserializeObject(m.JsonMetadata)).Color))                
                // .ForMember(dst => dst.Name, opts => opts.MapFrom(m => ((ItemDto)JsonConvert.DeserializeObject(m.JsonMetadata)).Name))
                // .ForMember(dst => dst.Price, opts => opts.MapFrom(m => ((ItemDto)JsonConvert.DeserializeObject(m.JsonMetadata)).Price));
        }
    }
}