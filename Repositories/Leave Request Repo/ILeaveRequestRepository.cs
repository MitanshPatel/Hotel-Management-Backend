using Hostel_Management.Models;
using System.Collections.Generic;

namespace Hostel_Management.Repositories
{
    public interface ILeaveRequestRepository
    {
        IEnumerable<LeaveRequest> GetAllLeaveRequests();
        IEnumerable<LeaveRequest> GetLeaveRequestsByStaffId(int staffId);
        LeaveRequest GetLeaveRequestById(int leaveRequestId);
        void AddLeaveRequest(LeaveRequest leaveRequest);
        void UpdateLeaveRequest(LeaveRequest leaveRequest);
        void SaveChanges();
    }
}
