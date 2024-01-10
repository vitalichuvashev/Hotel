using Hotell.Models;
using Hotell.Services.DTO;
using Hotell.Services.Interfaces;

namespace Hotell.Services
{
    public class RoomService : IRoomService
    {
        private const string DateFormat = "dd.MM.yyyy";
        private readonly HttpClient httpClient;
        public RoomService(HttpClient httpClient) => this.httpClient = httpClient;



        public async Task<DataTransfer<IEnumerable<Room>>> GetFreeRooms(DateTime startBooking, DateTime endBooking)
        {
            var rooms = await this.httpClient.GetFromJsonAsync<DataTransfer<IEnumerable<Room>>>($"api/Room?startBooking={startBooking.ToString(DateFormat)}&endBooking={endBooking.ToString(DateFormat)}");
            return rooms ?? DataTransfer<IEnumerable<Room>>.Empty();
        }
        public async Task<DataTransfer<IEnumerable<Room>>> GetRooms(int userID)
        {
            var rooms = await this.httpClient.GetFromJsonAsync<DataTransfer<IEnumerable<Room>>>($"api/Room/{userID}");
            return rooms ?? DataTransfer<IEnumerable<Room>>.Empty();
        }
    }
}
