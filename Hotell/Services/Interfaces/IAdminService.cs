using Hotell.Models;
using Hotell.Services.DTO;

namespace Hotell.Services.Interfaces
{
    public interface IAdminService
    {
        Task<DataTransfer<List<GuestBooking>>> GetGuestBookings();
    }
}
