using Microsoft.AspNetCore.Mvc;
using Hotel.Repository;
using Hotel.Repository.Interfaces;
using Entity = Hotel.Repository.Entity;
using Hotel.Repository.Entity;
using Hotel.WebApi.DTO;

namespace Hotel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IRoomRepository roomRepository;
        private readonly IUserRepository userRepository;

        public BookingController(IBookingRepository bookingRepository, IRoomRepository roomRepository, IUserRepository userRepository)
        {
            this.bookingRepository = bookingRepository;
            this.roomRepository = roomRepository;   
            this.userRepository = userRepository;
        }
        
        [HttpGet]
        public async Task<DataTransfer<List<GuestBooking>>> GetAll()
        {
            var bookings = await bookingRepository.GetAll();
            var roomIDs = bookings.Select<Booking, int>(booking => booking.RoomId);
            var userIds = bookings.Select<Booking, int>(booking => booking.UserId);

            var rooms = await roomRepository.GetRooms(roomIDs);
            var users = await userRepository.GetUsers(userIds);
            
            var guestBookings = new List<GuestBooking>();
            foreach(var booking in bookings)
            {
                var user = users.FirstOrDefault(u => u.Id== booking.UserId) ?? new();
                var room = rooms.FirstOrDefault(r => r.Id== booking.RoomId) ?? new();
                guestBookings.Add(new GuestBooking()
                {
                     Firstname= user.Firstname,
                     Lastname = user.Lastname,
                     Email= user.Email,
                     RoomNumber= room.RoomNumber,
                     StartDate= booking.Start,
                     EndDate= booking.End
                });
            }

            var dataTransfer = new DataTransfer<List<GuestBooking>>(guestBookings);
            return dataTransfer;
        }

        [HttpGet("{userID}")]
        public async Task<DTO.DataTransfer<IEnumerable<Entity.Booking>>> Get(int userID)
        {
            var bookings = await this.bookingRepository.GetByUserID(userID);
            var dataTransfer = new DTO.DataTransfer<IEnumerable<Entity.Booking>>(bookings);
            return dataTransfer;
        }


        [HttpPost]
        public DTO.DataTransfer<Entity.Booking> Post(DTO.DataTransfer<Entity.Booking> newBooking)
        {
            this.bookingRepository.AddNew(newBooking.Data);
            var dataTransfer = new DTO.DataTransfer<Entity.Booking>(newBooking.Data);
            return dataTransfer;
        }

        [HttpDelete("{id}")]
        public DTO.DataTransfer<Entity.Booking> Delete(int id)
        {
            var booking = this.bookingRepository.Delete(id);
            var dataTransfer = new DTO.DataTransfer<Entity.Booking>(booking ?? default!);
            if (booking != null)
            {
                return dataTransfer;
            }
            else
            {
                dataTransfer.ErrorMessage = "Can't remove booking";
                return dataTransfer;
            }
        }
    }
}
