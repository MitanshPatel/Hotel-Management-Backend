namespace Hostel_Management.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int GuestId { get; set; }
        public int? ReservationId { get; set; }
        public int RoomId { get; set; }
        public int Rating { get; set; } // Rating out of 5
        public string? Comments { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}

