using AutoMapper;
using FruitApi.Models.Domain;
using FruitApi.Models.DTOs;

namespace FruitApi.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<AddFruitDto, Fruit>().ReverseMap();
            CreateMap<Fruit, FruitDto>().ReverseMap();
        }
    }
}
