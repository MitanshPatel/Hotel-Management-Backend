using Hostel_Management.Models;
using System.Collections.Generic;

namespace Hostel_Management.Services
{
    public interface ILeaveRequestService
    {
        IEnumerable<LeaveRequest> GetAllLeaveRequests();
        IEnumerable<LeaveRequest> GetLeaveRequestsByStaffId(int staffId);
        LeaveRequest GetLeaveRequestById(int leaveRequestId);
        void AddLeaveRequest(LeaveRequest leaveRequest);
        void UpdateLeaveRequest(LeaveRequest leaveRequest);
        void ApproveLeaveRequest(int leaveRequestId);
        void RejectLeaveRequest(int leaveRequestId);
    }
}
