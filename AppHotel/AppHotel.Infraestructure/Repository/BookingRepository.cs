using AppHotel.Domain.DTOs;
using AppHotel.Domain.Entities;
using AppHotel.Domain.RepositoryContracts;
using AppHotel.Infraestructure.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AppHotel.Infraestructure.Repository
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        private readonly IBaseRepository<Room> _baseRepositoryRoom;
        private readonly IBaseRepository<Hotel> _baseRepositoryHotel;
        private readonly IBaseRepository<Guest> _baseRepositoryGuest;
        private readonly IBaseRepository<EmergencyContact> _baseRepositoryEmergencyContact;

        public BookingRepository(
            PersistenceContext context, IBaseRepository<Hotel> baseRepositoryHotel, 
            IBaseRepository<Room> baseRepositoryRoom, 
            IBaseRepository<Guest> baseRepositoryGuest,
            IBaseRepository<EmergencyContact> baseRepositoryEmergencyContact) : base(context)
        {
            _baseRepositoryHotel = baseRepositoryHotel;
            _baseRepositoryRoom = baseRepositoryRoom;
            _baseRepositoryGuest = baseRepositoryGuest;
            _baseRepositoryEmergencyContact = baseRepositoryEmergencyContact;
        }

        public async Task<List<Booking>> GetBookingByIdHotel(string? idHotel)
        {
            List<Booking> booking = (await _baseRepositoryHotel.GetAll()
                .Where(hotel => hotel.Id == idHotel)
                .Join(_baseRepositoryRoom.GetAll(),
                    hotel => hotel.Id,
                    room => room.HotelId,
                    (hotel, room) => new
                    {
                        room,
                        hotel
                    })
                .Join(this.GetAll(),
                    room => room.room.Id,
                    booking => booking.RoomId,
                    (room, booking) => new
                    {
                        room,
                        booking
                    })
                .ToListAsync())
                    .Select(x => {
                        x.booking.Room = x.room.room;
                        x.booking.Room.Hotel = x.room.hotel;
                        return x.booking;
                    }).ToList();

            return booking;
        }

        public async Task<List<Booking>> GetDetail(string? idBooking)
        {
            var bookingAux = (await this.GetAll()
                .Where(x => x.Id == idBooking)
                .GroupJoin(
                    _baseRepositoryGuest.GetAll(),
                    booking => booking.Id,
                    guest => guest.BookingId,
                    (booking, guests) => new
                    {
                        booking,
                        guests
                    })
                .ToListAsync());

            var guestBookingAux = bookingAux.SelectMany(
                x => x.guests.DefaultIfEmpty(),
                (booking, guest) => new
                {
                    booking.booking,
                    guest,
                    emergencyContact = _baseRepositoryEmergencyContact
                        .GetAll()
                        .FirstOrDefault(ec => ec.BookingId == booking.booking.Id)
                })
                .ToList();

            var booking = guestBookingAux.GroupBy(x => x.booking)
                .Select(x =>
                {
                    var bookingItem = x.Key;

                    if (bookingItem != null)
                    {
                        bookingItem.EmergencyContact = x.Select(y => y.emergencyContact).FirstOrDefault(z => z != null && z.BookingId == bookingItem.Id);
                        bookingItem.ListGuest = x.Select(y => y.guest).Where(y => y != null && y.BookingId == bookingItem.Id).ToList();
                    }

                    return bookingItem;
                })
                .Where(x => x != null)
                .ToList();

            return booking;
        }

        public async Task<List<BookingAvailableOutDTO>> GetAvailableBookings(BookingAvailableInDTO bookingAvailableInDTO)
        {
            List<BookingAvailableOutDTO> bookingAvailableOutDTO = new List<BookingAvailableOutDTO>();

            //var 

            return bookingAvailableOutDTO;
        }
    }
}
