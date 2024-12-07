using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hostel_Management.Models;
using Hostel_Management.Services;
using System.Security.Claims;
using Microsoft.VisualBasic;

namespace Hostel_Management.Controllers
{
    [ApiController]
    [Route("api/reservation")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [Authorize(Roles = "Guest")]
        [HttpGet]
        public IActionResult GetMyReservations()
        {
            var guestIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (guestIdClaim == null)
            {
                return Unauthorized();
            }
            var guestId = int.Parse(guestIdClaim);
            var reservations = _reservationService.GetReservationsByGuestId(guestId);
            return Ok(reservations);
        }

        [Authorize(Roles = "Guest")]
        [HttpGet("{reservationId}")]
        public IActionResult GetReservationById(int reservationId)
        {
            var guestIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (guestIdClaim == null)
            {
                return Unauthorized();
            }
            var reservation = _reservationService.GetReservationById(reservationId);
            if (reservation == null || reservation.GuestId != int.Parse(guestIdClaim))
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [Authorize(Roles = "Guest")]
        [HttpPost]
        public IActionResult AddReservation([FromBody] Reservation reservation)
        {
            var guestIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (guestIdClaim == null)
            {
                return Unauthorized();
            }
            reservation.GuestId = int.Parse(guestIdClaim);
            if (!_reservationService.BookRoom(reservation))
            {
                return BadRequest(new { msg = "Room is not available for the selected dates." });
            }
            return CreatedAtAction(nameof(GetReservationById), new { reservationId = reservation.ReservationId }, reservation);
        }

        [Authorize(Roles = "Guest")]
        [HttpPut("{reservationId}/cancel")]
        public IActionResult CancelReservation(int reservationId)
        {
            var guestIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (guestIdClaim == null)
            {
                return Unauthorized();
            }
            var reservation = _reservationService.GetReservationById(reservationId);
            if (reservation == null || reservation.GuestId != int.Parse(guestIdClaim))
            {
                return BadRequest();
            }

            reservation.Status = "Cancelled";
            _reservationService.UpdateReservation(reservation);
            return Ok(new { msg="Reservation Cancelled"});
        }

        //[Authorize(Roles = "Guest")]
        //[HttpDelete("{reservationId}")]
        //public IActionResult DeleteReservation(int reservationId)
        //{
        //    var guestIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (guestIdClaim == null)
        //    {
        //        return Unauthorized();
        //    }
        //    var reservation = _reservationService.GetReservationById(reservationId);
        //    if (reservation == null || reservation.GuestId != int.Parse(guestIdClaim))
        //    {
        //        return NotFound();
        //    }

        //    _reservationService.DeleteReservation(reservationId);
        //    return Ok("Reservation Deleted");
        //}

        //[Authorize(Roles = "Manager, Receptionist")]
        //[HttpGet("all-reservations")]
        //public IActionResult GetAllReservations()
        //{
        //    var reservations = _reservationService.GetAllReservations();
        //    return Ok(reservations);
        //}

        [Authorize(Roles = "Guest")]
        [HttpGet("room-availability-check")]
        public IActionResult CheckRoomAvailability(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            var (isAvailable, nextAvailableCheckIn, nextAvailableCheckOut) = _reservationService.GetNextAvailableTimes(roomId, checkInDate, checkOutDate);
            if (isAvailable)
            {
                return Ok(new { isAvailable });
            }
            return Ok(new { isAvailable, nextAvailableCheckIn, nextAvailableCheckOut });
        }

        [Authorize(Roles = "Guest")]
        [HttpGet("available-rooms")]
        public IActionResult GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        {
            var availableRooms = _reservationService.GetAvailableRooms(checkInDate, checkOutDate);
            return Ok(availableRooms);
        }
    }
}

