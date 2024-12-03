namespace Hostel_Management.Models
{
    public class StaffSchedule
    {
        public int ScheduleId { get; set; }
        public int StaffId { get; set; }
        public string ShiftType { get; set; }
        public DateTime ShiftStartTime { get; set; }
        public DateTime? ShiftEndTime { get; set; }
        public string Status { get; set; } // e.g., "Scheduled", "Started", "Ended", "Leave"
    }
}

