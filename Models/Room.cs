namespace Hostel_Management.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public required string RoomType { get; set; }
        public decimal Price { get; set; }
        public required string Status { get; set; }
        public required string BedType { get; set; }
        public required string View { get; set; }
        public bool Availability { get; set; }
    }
}
