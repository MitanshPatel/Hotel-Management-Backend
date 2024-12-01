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

        [Authorize(Roles = "Receptionist")]
        [HttpGet("all")]
        public IActionResult GetAllReservations()
        {
            var reservations = _reservationService.GetAllReservations();
            return Ok(reservations);
        }

        [Authorize(Roles = "Receptionist")]
        [HttpGet("pending")]
        public IActionResult GetPendingReservations()
        {
            var reservations = _reservationService.GetPendingReservations();
            return Ok(reservations);
        }

        [Authorize(Roles = "Receptionist")]
        [HttpGet("approved")]
        public IActionResult GetApprovedReservations()
        {
            var reservations = _reservationService.GetApprovedReservations();
            return Ok(reservations);
        }

        [Authorize(Roles = "Receptionist")]
        [HttpGet("rejected")]
        public IActionResult GetRejectedReservations()
        {
            var reservations = _reservationService.GetRejectedReservations();
            return Ok(reservations);
        }

        [Authorize(Roles = "Receptionist")]
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

        [Authorize(Roles = "Receptionist")]
        [HttpPut("{reservationId}/status")]
        public IActionResult UpdateReservationStatus(int reservationId, [FromBody] string status)
        {
            _reservationService.UpdateReservationStatus(reservationId, status);
            return Ok("Reservation status updated.");
        }
    }
}
