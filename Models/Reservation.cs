namespace Hostel_Management.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string? Status { get; set; }  // Pending, Approved, Rejected
        public string? PaymentStatus { get; set; }
        public string? SpecialRequests { get; set; }
    }
}

