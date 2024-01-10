using Hotel.Repository.Entity;
using Hotel.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly Database Database;
        public RoomRepository(Database database)=> this.Database = database;


        public async Task<IEnumerable<Room>> GetFreeRoms(DateTime startSearch, DateTime endSearch)
        {
            int count = await this.Database.Bookings.CountAsync();
            if(count > 0)
            {
                
                var freeRooms = await(from room in this.Database.Rooms
                                      where this.Database.Bookings.Any(b=>b.RoomId == room.Id) == false 
                                      select room).ToListAsync();

                var bookingFree =  (from room in this.Database.Rooms
                                         join booking in this.Database.Bookings
                                         on room.Id equals booking.RoomId
                                         where (startSearch.Date < booking.Start.Date && endSearch.Date <= booking.Start.Date) ||
                                         (startSearch.Date >= booking.End.Date)
                                        select room).ToList();

                freeRooms.AddRange(bookingFree);
                return freeRooms;
            }
            else
            {
                var result = await (from room in this.Database.Rooms select room).ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<Room>> GetRooms(int userID)
        {
            var rooms = await (from room in this.Database.Rooms
                         join booking in this.Database.Bookings
                         on room.Id equals booking.RoomId
                         where booking.UserId == userID
                         select room).ToListAsync();

            return rooms;
        }
        public async Task<IEnumerable<Room>> GetRooms(IEnumerable<int> roomIDs)
        {
            var rooms = await (from room in this.Database.Rooms
                               where roomIDs.Contains(room.Id)
                               select room).ToListAsync();

            return rooms;
        }
    }
}
