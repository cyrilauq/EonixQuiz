﻿using Microsoft.AspNetCore.Mvc;
using People.Application.DTOs;
using People.Application.DTOs.Args;
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

        [HttpDelete("{personId}")]
        public async Task<ActionResult> Delete(Guid personId)
        {
            await personService.Delete(personId);
            return NoContent();
        }

        [HttpGet("{personId}")]
        public async Task<PersonDTO> GetById(Guid personId)
        {
            return await personService.FindById(personId);
        }

        [HttpGet("list")]
        public async Task<PaginatedListDTOs<PersonDTO>> ListPeople([FromQuery] PaginatedArgsDTO? paginatedArgs = null, [FromQuery] FilteringPersonDTO? filteringArgs = null)
        {
            return await personService.GetAll(paginatedArgs, filteringArgs);
        }

        [HttpPut("{personId}")]
        public async Task<PersonDTO> UpdatePersonAsync(Guid personId, [FromBody] PersonDTO dto)
        {
            return await personService.UpdateAsync(personId, dto);
        }
    }
}
