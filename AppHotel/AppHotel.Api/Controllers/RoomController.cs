using AppHotel.ApplicationService.Exceptions;
using AppHotel.Domain.ApplicationServiceContracts;
using AppHotel.Domain.DTOs;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<RoomController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RoomController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoomController>
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

        // PUT api/<RoomController>/5
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

        // DELETE api/<RoomController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
