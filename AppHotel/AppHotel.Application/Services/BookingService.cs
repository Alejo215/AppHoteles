using AppHotel.ApplicationService.Exceptions;
using AppHotel.Domain.ApplicationServiceContracts;
using AppHotel.Domain.DTOs;
using AppHotel.Domain.Entities;
using AppHotel.Domain.RepositoryContracts;
using AutoMapper;

namespace AppHotel.ApplicationService.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _baseRepository;
        private readonly IBaseRepository<Guest> _guestRepository;
        private readonly IBaseRepository<EmergencyContact> _emergencyContactRepository;

        private readonly IMapper _mapper;

        private readonly INotifications _notifications;

        public BookingService(
            IBookingRepository baseRepository,
            IBaseRepository<Guest> guestRepository,
            IBaseRepository<EmergencyContact> emergencyContactRepository,
            IMapper mapper,
            INotifications notifications)
        {
            _baseRepository = baseRepository;
            _guestRepository = guestRepository;
            _emergencyContactRepository = emergencyContactRepository;
            _mapper = mapper;
            _notifications = notifications;
        }

        public async Task<BookingOutDTO> CreateBooking(BookingInDTO bookingInDTO)
        {
            //Meter codigo en una transacción - Ini
            Booking booking = _mapper.Map<Booking>(bookingInDTO);
            await _baseRepository.AddAsync(booking);
            BookingOutDTO bookingOutDTO = _mapper.Map<BookingOutDTO>(booking);

            List<Guest> listGuest = _mapper.Map<List<Guest>>(bookingInDTO.ListGuest);
            listGuest = listGuest.Select(x => { x.BookingId = booking.Id; return x; }).ToList();
            Task addListGuest = _guestRepository.AddManyAsync(listGuest);

            EmergencyContact emergencyContact = _mapper.Map<EmergencyContact>(bookingInDTO.EmergencyContact);
            emergencyContact.BookingId = booking.Id;
            Task addEmergencyContact = _emergencyContactRepository.AddAsync(emergencyContact);

            await Task.WhenAll(addListGuest, addEmergencyContact);

            _notifications.EmailNotification(listGuest.Select(x => x.Email));
            //Meter codigo en una transacción - Fin
            return bookingOutDTO;
        }

        public async Task<List<BookingOutDTO>> GetBooking(string? idHotel)
        {
            List<Booking> listBooking = await _baseRepository.GetBookingByIdHotel(idHotel);
            if(listBooking.Count == 0)
                throw new NotFoundApplicationException("No existe ninguna reserva para este hotel");

            List<BookingOutDTO> listBookingOutDTO = _mapper.Map<List<BookingOutDTO>>(listBooking);
            return listBookingOutDTO;
        }

        public async Task<List<BookingDetailDTO>> GetDetail(string? idBooking)
        {
            List<Booking?> listBooking = await _baseRepository.GetDetail(idBooking);
            if (listBooking.Count == 0)
                throw new NotFoundApplicationException("No existe ninguna reserva");

            List<BookingDetailDTO> listBookingDetailtDTO = _mapper.Map<List<BookingDetailDTO>>(listBooking);
            return listBookingDetailtDTO;
        }

        public async Task<List<BookingAvailableOutDTO>> GetAvailableBookings(BookingAvailableInDTO bookingAvailableInDTO)
        {

            List<BookingAvailableOutDTO> ListBookingAvailable = await _baseRepository.GetAvailableBookings(bookingAvailableInDTO);
            if(ListBookingAvailable.Count == 0)
                throw new NotFoundApplicationException("No hay habitaciones para reservas en esta fecha y/o esa cantidad de personas");

            return ListBookingAvailable;
        }
    }
}
