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
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IValidator<RoomInDTO> _validator;

        public RoomController(IRoomService roomService, IValidator<RoomInDTO> validator)
        {
            _roomService = roomService;
            _validator = validator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] RoomInDTO roomInDTO)
        {
            ValidationResult validation = await _validator.ValidateAsync(roomInDTO);
            if (!validation.IsValid)
                throw new BadRequestApplicationExeption(validation.ToString());

            RoomOutDTO result = await _roomService.CreateRoom(roomInDTO);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IActionResult> Put(string? id, [FromBody] RoomInUpdateDTO roomInUpdateDTO)
        {
            ValidationResult validation = await _validator.ValidateAsync(roomInUpdateDTO);
            if (!validation.IsValid)
                throw new BadRequestApplicationExeption(validation.ToString());

            RoomOutDTO result = await _roomService.UpdateRoom(id, roomInUpdateDTO);
            return Ok(result);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IActionResult> Patch(string? id, [FromQuery] bool Available)
        {
            RoomOutDTO result = await _roomService.UpdateAvailabilityRoom(id, Available);
            return Ok(result);
        }
    }
}
