using FluentValidation;
using AppHotel.Domain.DTOs;

namespace AppHotel.Domain.Validators
{
    public class GuestInValidator : AbstractValidator<GuestInDTO>
    {
        public GuestInValidator()
        {
            RuleFor(p => p.Email).NotEmpty().WithMessage("El correo es requerido").EmailAddress().WithMessage("Correo no valido");
        }
    }
}
