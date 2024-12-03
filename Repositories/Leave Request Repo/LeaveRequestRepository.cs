using Hostel_Management.Data;
using Hostel_Management.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hostel_Management.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<LeaveRequest> GetAllLeaveRequests()
        {
            return _context.LeaveRequests.ToList();
        }

        public IEnumerable<LeaveRequest> GetLeaveRequestsByStaffId(int staffId)
        {
            return _context.LeaveRequests.Where(lr => lr.StaffId == staffId).ToList();
        }

        public LeaveRequest GetLeaveRequestById(int leaveRequestId)
        {
            return _context.LeaveRequests.Find(leaveRequestId);
        }

        public void AddLeaveRequest(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Add(leaveRequest);
        }

        public void UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
