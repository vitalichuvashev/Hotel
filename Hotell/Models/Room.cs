namespace Hotell.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int Beds { get; set; }

        public int Price { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set;}
    }
}
