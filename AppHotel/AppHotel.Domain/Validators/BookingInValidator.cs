using FluentValidation;
using AppHotel.Domain.DTOs;

namespace AppHotel.Domain.Validators
{
    public class HotelInValidator : AbstractValidator<HotelInDTO>
    {
        public HotelInValidator() 
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage(p => $"\"{nameof(p.Name)}\" no puede ser vacio")
                .MaximumLength(100).WithMessage(p => $"\"{nameof(p.Name)}\" no puede puede superar los 100 digitos")
                .Matches(@"^[a-zA-Z0-9 ñÑ]+$").WithMessage(p => $"\"{nameof(p.Name)}\" debe ser alfanumerico");

            RuleFor(p => p.Location).NotEmpty().WithMessage(p => $"\"{nameof(p.Location)}\" no puede ser vacio")
                .MaximumLength(100).WithMessage(p => $"\"{nameof(p.Location)}\" no puede puede superar los 100 digitos")
                .Matches(@"^[a-zA-Z0-9 ñÑ]+$").WithMessage(p => $"\"{nameof(p.Location)}\" debe ser alfanumerico");
        }
    }
}
