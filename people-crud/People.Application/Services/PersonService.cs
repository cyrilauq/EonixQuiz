using AutoMapper;
using FluentValidation;
using People.Application.DTOs;
using ApplicationExceptions = People.Application.Exceptions;
using People.Application.Services.Interfaces;
using People.Domain.Entities;
using People.Domain.Exceptions;
using People.Domain.Repositories;
using People.Application.Exceptions;
using People.Application.DTOs.Args;
using System.Linq.Expressions;

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

        public async Task<PersonDTO> FindById(Guid personId)
        {
            Person? founded = await personRepository.GetById(personId);
            if (founded == null) throw new ResourceNotFound($"No person found for the id [{personId}]");
            return mapper.Map<PersonDTO>(founded);
        }

        public async Task<PaginatedListDTOs<PersonDTO>> GetAll(PaginatedArgsDTO? paginationArgs, FilteringPersonDTO? filteringArgs)
        {
            Expression<Func<Person, bool>>? filteringFunction = ComputeFilteringFunctionFromArgs(filteringArgs);
            IEnumerable<Person> result = await personRepository.GetAll(filteringFunction, paginationArgs == null ? null : mapper.Map<PaginatedArgs>(paginationArgs));
            return new PaginatedListDTOs<PersonDTO>(mapper.Map<IEnumerable<PersonDTO>>(result), paginationArgs?.PageSize, paginationArgs?.PageIndex);
        }

        private Expression<Func<Person, bool>>? ComputeFilteringFunctionFromArgs(FilteringPersonDTO? filteringArgs)
        {
            if (filteringArgs == null || (filteringArgs.firstName == null && filteringArgs.name == null)) return null;
            FilteringPersonDTO lowerCaseArgs = new FilteringPersonDTO(filteringArgs?.name?.ToLower(), filteringArgs?.firstName?.ToLower());
            return filteringArgs == null ? null :
                (Person person) =>
                    (lowerCaseArgs.firstName != null && person.Firstname.Contains(lowerCaseArgs.firstName))
                    || (lowerCaseArgs.name != null && person.Name.Contains(lowerCaseArgs.name));

            // TODO : make filter filter each field when they're both provided
        }
    }
}
