using AppHotel.Domain.DTOs;
using AppHotel.Domain.Entities;
using AppHotel.Domain.RepositoryContracts;
using AppHotel.Infraestructure.Configuration;
using MongoDB.Bson;
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

        public async Task<List<Booking?>> GetDetail(string? idBooking)
        {
            var bookingDataTask = this.GetAll()
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
                .ToListAsync();

            var emergencyContactsTask = _baseRepositoryEmergencyContact.GetAll()
                .Where(ec => ec.BookingId == idBooking)
                .ToListAsync();

            await Task.WhenAll(bookingDataTask, emergencyContactsTask);

            var  bookingData = bookingDataTask.Result;
            var emergencyContacts = emergencyContactsTask.Result;

            var combinedBookingData = bookingData
                .SelectMany(
                    x => x.guests.DefaultIfEmpty(),
                    (booking, guest) => new
                    {
                        booking.booking,
                        guest,
                        emergencyContact = emergencyContacts.FirstOrDefault(ec => ec.BookingId == booking.booking.Id)
                    })
                .GroupBy(x => x.booking)
                .Select(x =>
                {
                    var bookingItem = x.Key;

                    if (bookingItem != null)
                    {
                        bookingItem.EmergencyContact = x.Select(y => y.emergencyContact).FirstOrDefault(z => z != null && z.BookingId == bookingItem.Id);
                        bookingItem.ListGuest = x.Select(y => y.guest).Where(y => y != null && y.BookingId == bookingItem.Id).ToList()!;
                    }

                    return bookingItem;
                })
                .Where(x => x != null)
                .ToList();

            return combinedBookingData;
        }

        public async Task<List<BookingAvailableOutDTO>> GetAvailableBookings(BookingAvailableInDTO bookingAvailableInDTO)
        {
            var taskBookingAvailableAux = _baseRepositoryHotel.GetAll()
                .Where(hotel => hotel.Available && bookingAvailableInDTO.Location == hotel.Location)
                .Join(_baseRepositoryRoom.GetAll(),
                    hotel => hotel.Id,
                    room => room.HotelId,
                    (hotel, room) => new
                    {
                        room,
                        hotel
                    })
                .Where(x => x.room.Available && bookingAvailableInDTO.NumberPeople <= x.room.NumberPeople)
                .ToListAsync();

            var taskBookingIds = _baseRepositoryHotel.GetAll()
                .Where(hotel => hotel.Available && bookingAvailableInDTO.Location == hotel.Location)
                .Join(_baseRepositoryRoom.GetAll(),
                    hotel => hotel.Id,
                    room => room.HotelId,
                    (hotel, room) => new
                    {
                        room,
                        hotel
                    })
                .Where(x => x.room.Available && bookingAvailableInDTO.NumberPeople <= x.room.NumberPeople)
                .Join(this.GetAll(),
                    room => room.room.Id,
                    booking => booking.RoomId,
                    (room, booking) => new
                    {
                        room,
                        booking
                    })
                .Where(booking =>
                    (bookingAvailableInDTO.StartDate >= booking.booking.StartDate && bookingAvailableInDTO.StartDate <= booking.booking.EndDate) ||
                    (bookingAvailableInDTO.EndDate >= booking.booking.StartDate && bookingAvailableInDTO.EndDate <= booking.booking.EndDate) ||
                    (bookingAvailableInDTO.StartDate <= booking.booking.StartDate && bookingAvailableInDTO.EndDate >= booking.booking.EndDate))
                .Select(booking => booking.booking.RoomId)
                .ToListAsync();

            await Task.WhenAll(taskBookingAvailableAux, taskBookingIds);

            var bookingAvailableAuxResult = taskBookingAvailableAux.Result;
            var bookingIds = taskBookingIds.Result;

            var bookingAvailableAux = bookingAvailableAuxResult.Select(x =>
            {
                return new BookingAvailableOutDTO
                {
                    RoomId = x.room.Id,
                    Number = x.room.Number,
                    Cost = x.room.Cost,
                    Tax = x.room.Tax,
                    TypeRoom = x.room.TypeRoom,
                    NumberMaxPeople = x.room.NumberPeople,
                    Location = x.hotel.Location,
                    StartDate = bookingAvailableInDTO.StartDate,
                    EndDate = bookingAvailableInDTO.EndDate,
                };
            }).ToList();

            List<BookingAvailableOutDTO> bookingAvailableOutDTO = bookingAvailableAux.Where(x => !bookingIds.Contains(x.RoomId)).ToList();

            return bookingAvailableOutDTO;
        }
    }
}
