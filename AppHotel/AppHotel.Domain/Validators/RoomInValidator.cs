using FluentValidation;
using AppHotel.Domain.DTOs;

namespace AppHotel.Domain.Validators
{
    public class RoomInValidator : AbstractValidator<RoomInDTO>
    {
        public RoomInValidator()
        {
            RuleFor(p => p.HotelId).NotEmpty().WithMessage(p => $"\"{nameof(p.HotelId)}\" no puede ser vacio");

            RuleFor(p => p.TypeRoom).NotEmpty().WithMessage(p => $"\"{nameof(p.TypeRoom)}\" no puede ser vacio")
                .MaximumLength(255).WithMessage(p => $"\"{nameof(p.TypeRoom)}\" no puede puede superar los 255 digitos")
                .Matches(@"^[a-zA-Z0-9 ñÑ]+$").WithMessage(p => $"\"{nameof(p.TypeRoom)}\" debe ser alfanumerico");

            RuleFor(p => p.Location).NotEmpty().WithMessage(p => $"\"{nameof(p.Location)}\" no puede ser vacio")
                .MaximumLength(255).WithMessage(p => $"\"{nameof(p.Location)}\" no puede puede superar los 255 digitos")
                .Matches(@"^[a-zA-Z0-9 ñÑ]+$").WithMessage(p => $"\"{nameof(p.Location)}\" debe ser alfanumerico"); 
        }
    }
}
