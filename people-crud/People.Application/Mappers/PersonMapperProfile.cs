using AutoMapper;
using People.Application.DTOs;
using People.Domain.Entities;

namespace People.Application.Mappers
{
    public class PersonMapperProfile : Profile
    {
        public PersonMapperProfile() 
        {
            CreateMap<Person, PersonDTO>()
                .ReverseMap();
        }
    }
}
