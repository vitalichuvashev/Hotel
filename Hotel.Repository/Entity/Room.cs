
namespace Hotel.Repository.Entity
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int Beds { get; set; }

        public int Price { get; set; }
    }
}
