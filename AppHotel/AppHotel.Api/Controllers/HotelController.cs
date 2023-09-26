using AppHotel.Domain.ApplicationServiceContracts;
using AppHotel.Domain.DTOs;
using AppHotel.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService) 
        {
            _hotelService = hotelService;
        }

        // GET: api/<HotelController>
        [HttpGet]
        public async Task<Hotel> Get()
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            var mongoDatabase = mongoClient.GetDatabase("AppHotel");
            var hotelCollection = mongoDatabase.GetCollection<Hotel>("Hotel");
            Hotel hotel = await hotelCollection.Find( x => x.Id == "650f2bc67fe5a04fe468bcc6").FirstOrDefaultAsync();
            return hotel;
        }

        // GET api/<HotelController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HotelController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] HotelInDTO prestamoDTOIn)
        {
            HotelOutDTO result = await _hotelService.CreateHotel(prestamoDTOIn);
            return Ok(result);
        }

        // PUT api/<HotelController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HotelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
