using FluentValidation;
using Event.Application.Dtos.User.Request;

namespace Event.Application.Validators.User
{
    public class UserValidator : AbstractValidator<UserRequestDto>
    {
        public UserValidator() 
        { 
            RuleFor(c => c.UserName)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo Nombre no puede ser nulo");

        }

    }
}
