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

        private readonly IMapper _mapper;

        public BookingService(IBookingRepository baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;

        }

        public async Task<BookingOutDTO> CreateBooking(BookingInDTO bookingInDTO)
        {
            Booking booking = _mapper.Map<Booking>(bookingInDTO);
            await _baseRepository.AddAsync(booking);
            BookingOutDTO bookingOutDTO = _mapper.Map<BookingOutDTO>(booking);
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
                throw new NotFoundApplicationException("No hay habitaciones para reservas en esta fecha");

            return ListBookingAvailable;
        }
    }
}
