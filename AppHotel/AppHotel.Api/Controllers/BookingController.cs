using AppHotel.ApplicationService.Exceptions;
using AppHotel.Domain.ApplicationServiceContracts;
using AppHotel.Domain.DTOs;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace AppHotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IValidator<BookingInDTO> _validator;
        private readonly IValidator<GuestInDTO> _validatorGuest;
        private readonly IValidator<BookingAvailableInDTO> _validatorAvailable;

        public BookingController(IBookingService bookingService,
                                IValidator<BookingInDTO> validator,
                                IValidator<GuestInDTO> validatorGuest,
                                IValidator<BookingAvailableInDTO> validatorAvailable)
        {
            _bookingService = bookingService;
            _validator = validator;
            _validatorGuest = validatorGuest;
            _validatorAvailable = validatorAvailable;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] BookingInDTO bookingInDTO)
        {
            ValidationResult validation = await _validator.ValidateAsync(bookingInDTO);
            if (!validation.IsValid)
                throw new BadRequestApplicationExeption(validation.ToString());

            foreach(var guestInDTO  in bookingInDTO.ListGuest!)
            {
                ValidationResult validationGuest = await _validatorGuest.ValidateAsync(guestInDTO);
                if (!validationGuest.IsValid)
                    throw new BadRequestApplicationExeption(validation.ToString());
            }

            BookingOutDTO result = await _bookingService.CreateBooking(bookingInDTO);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("byIdHotel/{idHotel}")]
        public async Task<IActionResult> GetByIdHotel(string? idHotel)
        {
            List<BookingOutDTO> result = await _bookingService.GetBooking(idHotel);
            return Ok(result); ;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("byIdBooking/{idBooking}")]
        public async Task<IActionResult> GetByIdBooking(string? idBooking)
        {
            List<BookingDetailDTO> result = await _bookingService.GetDetail(idBooking);
            return Ok(result); ;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("byAvailability")]
        public async Task<IActionResult> GetAvailableBookings([FromQuery] BookingAvailableInDTO bookingAvailableInDTO)
        {
            ValidationResult validation = await _validatorAvailable.ValidateAsync(bookingAvailableInDTO);
            if (!validation.IsValid)
                throw new BadRequestApplicationExeption(validation.ToString());

            List<BookingAvailableOutDTO> result = await _bookingService.GetAvailableBookings(bookingAvailableInDTO);
            return Ok(result); ;
        }
    }
}
