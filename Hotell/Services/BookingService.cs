using Hotell.Models;
using Hotell.Services.DTO;
using Hotell.Services.Interfaces;

namespace Hotell.Services
{
    public class BookingService : IBookingService
    {
        private readonly HttpClient httpClient;
        public BookingService(HttpClient httpClient) => this.httpClient = httpClient;


        public async Task<DataTransfer<Booking>> AddNew(Booking newBooking)
        {
            var dataTransfer = new DataTransfer<Booking>(newBooking);
            var response = await this.httpClient.PostAsJsonAsync<DataTransfer<Booking>>("api/booking", dataTransfer);

            var data = await response.Content.ReadFromJsonAsync<DataTransfer<Booking>>();

            return data ?? DataTransfer<Booking>.Empty();
        }

        public async Task<DataTransfer<IEnumerable<Booking>>> GetBookings(int userID)
        {
            var bookingData = await this.httpClient.GetFromJsonAsync<DataTransfer<IEnumerable<Booking>>>($"api/Booking/{userID}");

            var roomData = await this.httpClient.GetFromJsonAsync<DataTransfer<IEnumerable<Room>>>($"api/Room/{userID}");

            if(bookingData != null && roomData != null)
            {
                if(roomData.IsSuccess && bookingData.IsSuccess)
                {
                    foreach (var booking in bookingData.Data)
                    {
                        var room = roomData.Data.FirstOrDefault(room=>room.Id == booking.RoomId);
                        if(room != null)
                        {
                            booking.BedsAmount = room.Beds;
                            booking.Price = room.Price;
                            booking.RoomNumber = room.RoomNumber;   
                        }
                    }
                }
                
            }
            return bookingData ?? DataTransfer<IEnumerable<Booking>>.Empty();
        }
        public async Task<DataTransfer<Booking>> RemoveBooking(int id)
        {
            var dataTransfer = await this.httpClient.DeleteFromJsonAsync<DataTransfer<Booking>>($"api/Booking/{id}");

            return dataTransfer ?? DataTransfer<Booking>.Empty();
        }
    }
}
