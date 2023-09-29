using FluentValidation;
using FluentValidation.Results;
using AppHotel.ApplicationService.Exceptions;
using AppHotel.Domain.ApplicationServiceContracts;
using AppHotel.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AppHotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly IValidator<HotelInDTO> _validator;

        public HotelController(IHotelService hotelService, IValidator<HotelInDTO> validator) 
        {
            _hotelService = hotelService;
            _validator = validator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] HotelInDTO hotelInDTO)
        {
            ValidationResult validation = await _validator.ValidateAsync(hotelInDTO);
            if (!validation.IsValid)
                throw new BadRequestApplicationExeption(validation.ToString());

            HotelOutDTO result = await _hotelService.CreateHotel(hotelInDTO);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IActionResult> Put(string? id, [FromBody] HotelInUpdateDTO hotelInUpdateDTO)
        {
            ValidationResult validation = await _validator.ValidateAsync(hotelInUpdateDTO);
            if (!validation.IsValid)
                throw new BadRequestApplicationExeption(validation.ToString());

            HotelOutDTO result = await _hotelService.UpdateHotel(id, hotelInUpdateDTO);
            return Ok(result);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IActionResult> Patch(string? id, [FromQuery] bool Available)
        {
            HotelOutDTO result = await _hotelService.UpdateAvailabilityHotel(id, Available);
            return Ok(result);
        }
    }
}
