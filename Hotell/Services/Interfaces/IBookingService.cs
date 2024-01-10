using Hotell.Models;
using Hotell.Services.DTO;

namespace Hotell.Services.Interfaces
{
    public interface IBookingService
    {
        Task<DataTransfer<Booking>> AddNew(Booking newBooking);
        Task<DataTransfer<IEnumerable<Booking>>> GetBookings(int userID);
        Task<DataTransfer<Booking>> RemoveBooking(int id);
    }
}
