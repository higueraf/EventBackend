using FluentValidation;
using Event.Application.Dtos.Occurrence.Request;

namespace Event.Application.Validators.Occurrence
{
    public class OccurrenceValidator : AbstractValidator<OccurrenceRequestDto>
    {
        public OccurrenceValidator() 
        { 
            RuleFor(c => c.Description)
                .NotNull().WithMessage("El campo Descripción no puede ser nulo")
                .NotEmpty().WithMessage("El campo Descripción no puede ser nulo");

        }

    }
}
