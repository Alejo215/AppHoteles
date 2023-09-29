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

        public BookingController(IBookingService bookingService, IValidator<BookingInDTO> validator)
        {
            _bookingService = bookingService;
            _validator = validator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] BookingInDTO bookingInDTO)
        {
            ValidationResult validation = await _validator.ValidateAsync(bookingInDTO);
            if (!validation.IsValid)
                throw new BadRequestApplicationExeption(validation.ToString());

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
    }
}
