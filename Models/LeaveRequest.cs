namespace Hostel_Management.Models
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; set; }
        public int StaffId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } // e.g., "Leave Requested", "Approved", "Rejected"
    }
}

