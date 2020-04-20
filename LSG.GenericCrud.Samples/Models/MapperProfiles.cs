using System;
using AutoMapper;
using LSG.GenericCrud.Samples.Models.DTOs;
using LSG.GenericCrud.Samples.Models.Entities;
using Newtonsoft.Json;

namespace LSG.GenericCrud.Samples.Models
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<ItemDto, Item>()
                .ForMember(dst => dst.JsonMetadata, opts => { opts.MapFrom(m => JsonConvert.SerializeObject(new { m.Color, m.Price, m.Name}));});
            CreateMap<Item, ItemDto>()
                .ForMember(dst => dst.Color, opts => { opts.MapFrom(m => JsonConvert.DeserializeObject<ItemDto>(m.JsonMetadata).Color);})
                .ForMember(dst => dst.Name,  opts => { opts.MapFrom(m => JsonConvert.DeserializeObject<ItemDto>(m.JsonMetadata).Name);})
                .ForMember(dst => dst.Price, opts => { opts.MapFrom(m => JsonConvert.DeserializeObject<ItemDto>(m.JsonMetadata).Price);});

            CreateMap<UserDto, User>()                
                .ForMember(dst => dst.SomeSecretValue, opts => opts.MapFrom(new UserSecretResolver()));
            CreateMap<User, UserDto>();

            CreateMap<BlogPostDto, BlogPost>()
                .ForMember(dst => dst.Hash, opts => opts.MapFrom(new HashResolver()));
            CreateMap<BlogPost, BlogPostDto>();

        }
    }

    public class UserSecretResolver : IValueResolver<UserDto, User, string>
    {
        public string Resolve(UserDto source, User destination, string destMember, ResolutionContext context)
        {
            string text = new Random().Next().ToString();
            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            string encoded = Convert.ToBase64String(textBytes);
            return encoded;
        }
    }
    
    public class HashResolver : IValueResolver<BlogPostDto, BlogPost, string>
    {
        public string Resolve(BlogPostDto source, BlogPost destination, string destMember, ResolutionContext context)
        {
            return source.GetHashCode().ToString();
        }
    }
}
