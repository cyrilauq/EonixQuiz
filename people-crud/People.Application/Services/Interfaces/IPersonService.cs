using People.Application.DTOs;

namespace People.Application.Services.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDTO> Add(PersonDTO personDTO);
    }
}
