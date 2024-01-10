namespace Hotell.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int BedsAmount { get; set; }

        public int Price { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int UserId { get; set; }

        public int RoomId { get; set; }
    }
}
