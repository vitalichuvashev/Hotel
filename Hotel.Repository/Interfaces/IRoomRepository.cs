using Hotel.Repository.Entity;

namespace Hotel.Repository.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetFreeRoms(DateTime startSearch, DateTime endSearch);
        Task<IEnumerable<Room>> GetRooms(int userID);

        Task<IEnumerable<Room>> GetRooms(IEnumerable<int> roomIDs);
    }
}
