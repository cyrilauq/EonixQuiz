using AutoMapper;
using FluentValidation;
using People.Application.DTOs;
using ApplicationExceptions = People.Application.Exceptions;
using People.Application.Services.Interfaces;
using People.Domain.Entities;
using People.Domain.Exceptions;
using People.Domain.Repositories;
using People.Application.Exceptions;

namespace People.Application.Services
{
    public class PersonService(IPersonRepository personRepository, IMapper mapper, IValidator<PersonDTO> validator) : IPersonService
    {
        public async Task<PersonDTO> Add(PersonDTO personDTO)
        {
            try
            {
                var validationResult = validator.Validate(personDTO);
                if (!validationResult.IsValid)
                {
                    throw new ApplicationExceptions.ValidationException("Entity not valid", validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
                }
                Person entity = mapper.Map<Person>(personDTO);
                Person addingResult = await personRepository.Add(entity);
                return mapper.Map<PersonDTO>(addingResult);
            }
            catch (EntityNotValidException enve)
            {
                throw new ApplicationExceptions.ValidationException("Entity not valid", ["Firstname and Lastname should have a value"]);
            }
        }

        public async Task<bool> Delete(Guid personId)
        {
            try
            {
                return await personRepository.Delete(personId);
            }
            catch (NotSuchEntityFoundException)
            {
                throw new ResourceNotFound($"No person found for the id [{personId}]");
            }
        }
    }
}
