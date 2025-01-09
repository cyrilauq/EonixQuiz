using Microsoft.AspNetCore.Mvc;
using People.Application.DTOs;
using People.Application.Services.Interfaces;

namespace People.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController(IPersonService personService) : ControllerBase
    {
        [HttpPost]
        public async Task<PersonDTO> Add(PersonDTO dto)
        {
            return await personService.Add(dto);
        }
    }
}
