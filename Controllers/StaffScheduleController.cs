using Hostel_Management.Models;
using Hostel_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hostel_Management.Controllers
{
    [ApiController]
    [Route("api/staff-schedule")]
    public class StaffScheduleController : ControllerBase
    {
        private readonly IStaffScheduleService _staffScheduleService;
        private readonly IStaffShiftService _staffShiftService;

        public StaffScheduleController(IStaffScheduleService staffScheduleService, IStaffShiftService staffShiftService)
        {
            _staffScheduleService = staffScheduleService;
            _staffShiftService = staffShiftService;
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public IActionResult GetAllSchedules()
        {
            var schedules = _staffScheduleService.GetAllSchedules();
            return Ok(schedules);
        }

        [Authorize(Roles = "Receptionist, Housekeeping")]
        [HttpGet("my-schedule")]
        public IActionResult GetMySchedule()
        {
            var staffIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (staffIdClaim == null)
            {
                return Unauthorized();
            }
            var staffId = int.Parse(staffIdClaim);
            var schedule = _staffShiftService.GetShiftByStaffId(staffId);
            return Ok(schedule);
        }

        [Authorize(Roles = "Receptionist, Housekeeping")]
        [HttpGet("my-attendance-history")]
        public IActionResult GetMyAttendanceHistory()
        {
            var staffIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (staffIdClaim == null)
            {
                return Unauthorized();
            }
            var staffId = int.Parse(staffIdClaim);
            var schedules = _staffScheduleService.GetSchedulesByStaffId(staffId);
            return Ok(schedules);
        }

        [Authorize(Roles = "Receptionist, Housekeeping")]
        [HttpPut("start")]
        public IActionResult StartShift()
        {
            var staffIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (staffIdClaim == null)
            {
                return Unauthorized();
            }
            var staffId = int.Parse(staffIdClaim);
            var shift = _staffShiftService.GetShiftByStaffId(staffId);
            if (shift == null)
            {
                return NotFound("Shift not found for the staff.");
            }

            var schedule = new StaffSchedule
            {
                StaffId = staffId,
                ShiftType = shift.ShiftType,
                ShiftStartTime = DateTime.Now,
                Status = "Started"
            };

            _staffScheduleService.AddSchedule(schedule);

            return Ok(new { scheduleId = schedule.ScheduleId, message = "Shift Started" });
        }

        [Authorize(Roles = "Receptionist, Housekeeping")]
        [HttpPut("{scheduleId}/end")]
        public IActionResult EndShift(int scheduleId)
        {
            var staffIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (staffIdClaim == null)
            {
                return Unauthorized();
            }
            var staffId = int.Parse(staffIdClaim);
            var schedule = _staffScheduleService.GetSchedulesByStaffId(staffId).FirstOrDefault(s => s.ScheduleId == scheduleId);
            if (schedule == null)
            {
                return NotFound();
            }

            schedule.Status = "Ended";
            schedule.ShiftEndTime = DateTime.Now; // Update the end time to the current time
            _staffScheduleService.UpdateSchedule(schedule);
            return Ok("Shift Ended");
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpGet("shifts")]
        public IActionResult GetAllShifts()
        {
            var shifts = _staffShiftService.GetAllShifts();
            return Ok(shifts);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost("shifts")]
        public IActionResult AddShift([FromBody] StaffShift shift)
        {
            _staffShiftService.AddShift(shift);
            return CreatedAtAction(nameof(GetAllShifts), new { shiftId = shift.StaffShiftId }, shift);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPut("shifts/{shiftId}")]
        public IActionResult UpdateShift(int shiftId, [FromBody] StaffShift shift)
        {
            if (shiftId != shift.StaffShiftId)
            {
                return BadRequest();
            }

            _staffShiftService.UpdateShift(shift);
            return Ok("Shift Updated");
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpDelete("shifts/{shiftId}")]
        public IActionResult DeleteShift(int shiftId)
        {
            _staffShiftService.DeleteShift(shiftId);
            return Ok("Shift Deleted");
        }
    }
}


