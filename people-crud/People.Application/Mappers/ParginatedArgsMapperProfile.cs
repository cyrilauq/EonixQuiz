using AutoMapper;
using People.Application.DTOs.Args;
using People.Domain.Repositories;

namespace People.Application.Mappers
{
    public class ParginatedArgsMapperProfile : Profile
    {
        public ParginatedArgsMapperProfile()
        {
            CreateMap<PaginatedArgs, PaginatedArgsDTO>()
                .ReverseMap();
        }
    }
}
