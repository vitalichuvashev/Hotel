using Hotel.Repository.Entity;
using Hotel.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly Database Database;

        public BookingRepository(Database database)=> this.Database = database;

        public async Task<IEnumerable<Booking>> GetAll()
        {
            return await this.Database.Bookings.ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByUserID(int userID) {

            return await this.Database.Bookings.Where(booking=>booking.UserId == userID).ToListAsync();
        }

        public void AddNew(Booking booking)
        {
            
            this.Database.Bookings.Add(booking);
            this.Database.SaveChanges();
        }
        public Booking? Delete(int id)
        {
            var booking = this.Database.Bookings.FirstOrDefault(booking => booking.Id == id);
            if(booking != null)
            {
                this.Database.Bookings.Remove(booking);
                this.Database.SaveChanges();
                return booking;
            }
            return null;
        }
    }
}
