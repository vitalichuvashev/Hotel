using Hotell.Models;
using Hotell.Services.Interfaces;
using Hotell.Services.DTO;

namespace Hotell.Services
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient httpClient;
        public AdminService(HttpClient httpClient) => this.httpClient = httpClient;


        public async Task<DataTransfer<List<GuestBooking>>> GetGuestBookings()
        {
            var guestBookins = await this.httpClient.GetFromJsonAsync<DataTransfer<List<GuestBooking>>>($"api/booking");
            return guestBookins ?? DataTransfer<List<GuestBooking>>.Empty();
        }
    }
}
