using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hostel_Management.Services;

namespace Hostel_Management.Controllers
{
    [ApiController]
    [Route("api/reservation-management")]
    public class ReservationManagementController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationManagementController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [Authorize(Roles = "Receptionist, Manager")]
        [HttpGet("all")]
        public IActionResult GetAllReservations()
        {
            var reservations = _reservationService.GetAllReservations();
            return Ok(reservations);
        }

        [Authorize(Roles = "Receptionist, Manager")]
        [HttpGet("pending")]
        public IActionResult GetPendingReservations()
        {
            var reservations = _reservationService.GetPendingReservations();
            return Ok(reservations);
        }

        [Authorize(Roles = "Receptionist, Manager")]
        [HttpGet("approved")]
        public IActionResult GetApprovedReservations()
        {
            var reservations = _reservationService.GetApprovedReservations();
            return Ok(reservations);
        }

        [Authorize(Roles = "Receptionist, Manager")]
        [HttpGet("rejected")]
        public IActionResult GetRejectedReservations()
        {
            var reservations = _reservationService.GetRejectedReservations();
            return Ok(reservations);
        }

        [Authorize(Roles = "Receptionist, Manager")]
        [HttpGet("{reservationId}")]
        public IActionResult GetReservationById(int reservationId)
        {
            var reservation = _reservationService.GetReservationById(reservationId);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [Authorize(Roles = "Receptionist, Manager")]
        [HttpPut("{reservationId}/status")]
        public IActionResult UpdateReservationStatus(int reservationId, [FromBody] UpdateReservationStatusDto statusDto)
        {
            _reservationService.UpdateReservationStatus(reservationId, statusDto.Status);
            return Ok(new { msg = "Reservation status updated." });
        }
    }
}

public class UpdateReservationStatusDto
{
    public required string Status { get; set; }
}
