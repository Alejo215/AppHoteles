using FluentValidation;
using AppHotel.Domain.DTOs;

namespace AppHotel.Domain.Validators
{
    public class BookingAvailableInValidator : AbstractValidator<BookingAvailableInDTO>
    {
        public BookingAvailableInValidator() 
        {
            RuleFor(p => p.Location).NotEmpty().WithMessage(p => $"\"{nameof(p.Location)}\" no puede ser vacio")
                .MaximumLength(100).WithMessage(p => $"\"{nameof(p.Location)}\" no puede puede superar los 100 digitos")
                .Matches(@"^[a-zA-Z0-9 ñÑ]+$").WithMessage(p => $"\"{nameof(p.Location)}\" debe ser alfanumerico");
        }
    }
}
