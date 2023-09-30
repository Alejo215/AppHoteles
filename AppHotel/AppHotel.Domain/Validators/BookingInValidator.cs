using FluentValidation;
using AppHotel.Domain.DTOs;

namespace AppHotel.Domain.Validators
{
    public class BookingInValidator : AbstractValidator<BookingInDTO>
    {
        public BookingInValidator()
        {
            RuleFor(p => p.Description).NotEmpty().WithMessage(p => $"\"{nameof(p.Description)}\" no puede ser vacio")
                .MaximumLength(100).WithMessage(p => $"\"{nameof(p.Description)}\" no puede puede superar los 255 digitos")
                .Matches(@"^[a-zA-Z0-9 ñÑ]+$").WithMessage(p => $"\"{nameof(p.Description)}\" debe ser alfanumerico");

            RuleFor(p => p.ListGuest).NotEmpty().WithMessage(p => $"\"{nameof(p.ListGuest)}\" no puede ser vacio");

            RuleFor(p => p.EmergencyContact).NotEmpty().WithMessage(p => $"\"{nameof(p.EmergencyContact)}\" no puede ser vacio");
        }
    }
}
