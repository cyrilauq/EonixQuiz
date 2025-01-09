using AutoMapper;
using People.Application.DTOs;
using People.Application.Exceptions;
using People.Application.Services.Interfaces;
using People.Domain.Entities;
using People.Domain.Exceptions;
using People.Domain.Repositories;

namespace People.Application.Services
{
    public class PersonService(IPersonRepository personRepository, IMapper mapper) : IPersonService
    {
        public async Task<PersonDTO> Add(PersonDTO personDTO)
        {
            try
            {
                Person entity = mapper.Map<Person>(personDTO);
                Person addingResult = await personRepository.Add(entity);
                return mapper.Map<PersonDTO>(addingResult);
            }
            catch (EntityNotValidException enve)
            {
                throw new ValidationException("Entity not valid", ["Firstname and Lastname should have a value"]);
            }
        }
    }
}
