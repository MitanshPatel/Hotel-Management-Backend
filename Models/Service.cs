namespace Hostel_Management.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceType { get; set; }
        public int GuestId { get; set; }
        public int ReservationId { get; set; } 
        public int RoomId { get; set; } 
        public int? HousekeepingId { get; set; } 
        public DateTime RequestTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public string? Status { get; set; }
    }
}
