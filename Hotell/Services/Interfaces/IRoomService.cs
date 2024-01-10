using Hotell.Models;
using Hotell.Services.DTO;
namespace Hotell.Services.Interfaces
{
    public interface IRoomService
    {
        Task<DataTransfer<IEnumerable<Room>>> GetFreeRooms(DateTime startBooking, DateTime endBooking);
        Task<DataTransfer<IEnumerable<Room>>> GetRooms(int userID);
    }
}
