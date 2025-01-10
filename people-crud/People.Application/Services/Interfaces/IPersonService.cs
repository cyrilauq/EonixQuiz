using People.Application.DTOs;
using People.Application.DTOs.Args;

namespace People.Application.Services.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDTO> Add(PersonDTO personDTO);
        Task<bool> Delete(Guid personId);
        Task<PaginatedListDTOs<PersonDTO>> GetAll(PaginatedArgsDTO? parginationArgs, FilteringPersonDTO? filteringArgs);
    }
}
