using Hostel_Management.Models;
using Hostel_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hostel_Management.Controllers
{
    [ApiController]
    [Route("api/leave-request")]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public IActionResult GetAllLeaveRequests()
        {
            var leaveRequests = _leaveRequestService.GetAllLeaveRequests();
            return Ok(leaveRequests);
        }

        [Authorize(Roles = "Receptionist, Housekeeping")]
        [HttpGet("my-leave-requests")]
        public IActionResult GetMyLeaveRequests()
        {
            var staffIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (staffIdClaim == null)
            {
                return Unauthorized();
            }
            var staffId = int.Parse(staffIdClaim);
            var leaveRequests = _leaveRequestService.GetLeaveRequestsByStaffId(staffId);
            return Ok(leaveRequests);
        }

        [Authorize(Roles = "Receptionist, Housekeeping")]
        [HttpPost]
        public IActionResult AddLeaveRequest([FromBody] LeaveRequest leaveRequest)
        {
            var staffIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (staffIdClaim == null)
            {
                return Unauthorized();
            }
            leaveRequest.StaffId = int.Parse(staffIdClaim);
            leaveRequest.Status = "Leave Requested";
            _leaveRequestService.AddLeaveRequest(leaveRequest);
            return CreatedAtAction(nameof(GetMyLeaveRequests), new { leaveRequestId = leaveRequest.LeaveRequestId }, leaveRequest);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPut("{leaveRequestId}/approve")]
        public IActionResult ApproveLeaveRequest(int leaveRequestId)
        {
            _leaveRequestService.ApproveLeaveRequest(leaveRequestId);
            return Ok(new { msg = "Leave Request Approved" });
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPut("{leaveRequestId}/reject")]
        public IActionResult RejectLeaveRequest(int leaveRequestId)
        {
            _leaveRequestService.RejectLeaveRequest(leaveRequestId);
            return Ok(new { msg = "Leave Request Rejected" });
        }
    }
}
