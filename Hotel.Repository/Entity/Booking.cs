
namespace Hotel.Repository.Entity
{
    public class Booking
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoomId { get; set; }
  
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}
