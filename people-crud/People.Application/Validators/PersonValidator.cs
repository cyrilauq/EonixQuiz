using FluentValidation;
using People.Application.DTOs;

namespace People.Application.Validators
{
    public class PersonValidator : AbstractValidator<PersonDTO>
    {
        public PersonValidator()
        {
            RuleFor(dto => dto.Firstname).NotEmpty();
            RuleFor(dto => dto.Name).NotEmpty();
        }
    }
}
