namespace Hostel_Management.Models
{
    public class StaffShift
    {
        public int StaffShiftId { get; set; }
        public int StaffId { get; set; } // Same as UserId
        public string ShiftType { get; set; } // e.g., "Morning", "Afternoon", "Night"
    }
}

