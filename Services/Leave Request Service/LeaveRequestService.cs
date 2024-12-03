using Hostel_Management.Models;
using Hostel_Management.Repositories;
using System.Collections.Generic;

namespace Hostel_Management.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IStaffScheduleService _staffScheduleService;

        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IStaffScheduleService staffScheduleService)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _staffScheduleService = staffScheduleService;
        }

        public IEnumerable<LeaveRequest> GetAllLeaveRequests()
        {
            return _leaveRequestRepository.GetAllLeaveRequests();
        }

        public IEnumerable<LeaveRequest> GetLeaveRequestsByStaffId(int staffId)
        {
            return _leaveRequestRepository.GetLeaveRequestsByStaffId(staffId);
        }

        public LeaveRequest GetLeaveRequestById(int leaveRequestId)
        {
            return _leaveRequestRepository.GetLeaveRequestById(leaveRequestId);
        }

        public void AddLeaveRequest(LeaveRequest leaveRequest)
        {
            _leaveRequestRepository.AddLeaveRequest(leaveRequest);
            _leaveRequestRepository.SaveChanges();
        }

        public void UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            _leaveRequestRepository.UpdateLeaveRequest(leaveRequest);
            _leaveRequestRepository.SaveChanges();
        }

        public void ApproveLeaveRequest(int leaveRequestId)
        {
            var leaveRequest = _leaveRequestRepository.GetLeaveRequestById(leaveRequestId);
            if (leaveRequest != null)
            {
                leaveRequest.Status = "Approved";
                _leaveRequestRepository.UpdateLeaveRequest(leaveRequest);
                _leaveRequestRepository.SaveChanges();

                // Add leave entries to StaffSchedule
                _staffScheduleService.AddLeaveEntries(leaveRequest.StaffId, leaveRequest.StartDate, leaveRequest.EndDate);
            }
        }

        public void RejectLeaveRequest(int leaveRequestId)
        {
            var leaveRequest = _leaveRequestRepository.GetLeaveRequestById(leaveRequestId);
            if (leaveRequest != null)
            {
                leaveRequest.Status = "Rejected";
                _leaveRequestRepository.UpdateLeaveRequest(leaveRequest);
                _leaveRequestRepository.SaveChanges();
            }
        }
    }
}
