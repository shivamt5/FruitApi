using AutoMapper;
using FruitApi.Commands;
using FruitApi.Models.Domain;
using FruitApi.Models.DTOs;

namespace FruitApi.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<CreateFruitCommand, Fruit>().ReverseMap();
            CreateMap<Fruit, FruitDto>().ReverseMap();
            CreateMap<FruitDto, CreateFruitCommand>().ReverseMap();
        }
    }
}
