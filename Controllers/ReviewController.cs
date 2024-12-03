using Hostel_Management.Models;
using Hostel_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hostel_Management.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize(Roles = "Guest")]
        [HttpGet("my-reviews")]
        public IActionResult GetMyReviews()
        {
            var guestIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (guestIdClaim == null)
            {
                return Unauthorized();
            }
            var guestId = int.Parse(guestIdClaim);
            var reviews = _reviewService.GetReviewsByGuestId(guestId);
            return Ok(reviews);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{reviewId}")]
        public IActionResult GetReviewById(int reviewId)
        {
            var guestIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (guestIdClaim == null)
            {
                return Unauthorized();
            }
            var review = _reviewService.GetReviewById(reviewId);
            if (review == null || review.GuestId != int.Parse(guestIdClaim))
            {
                return NotFound();
            }
            return Ok(review);
        }

        [Authorize(Roles = "Guest")]
        [HttpPost]
        public IActionResult AddReview([FromBody] Review review)
        {
            var guestIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (guestIdClaim == null)
            {
                return Unauthorized();
            }
            review.GuestId = int.Parse(guestIdClaim);
            review.ReviewDate = DateTime.Now;
            _reviewService.AddReview(review);
            return CreatedAtAction(nameof(GetReviewById), new { reviewId = review.ReviewId }, review);
        }

        [Authorize(Roles = "Guest")]
        [HttpPut("{reviewId}")]
        public IActionResult UpdateReview(int reviewId, [FromBody] Review review)
        {
            var guestIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (guestIdClaim == null)
            {
                return Unauthorized();
            }
            if (reviewId != review.ReviewId || review.GuestId != int.Parse(guestIdClaim))
            {
                return BadRequest();
            }

            _reviewService.UpdateReview(review);
            return Ok("Review Updated");
        }

        [Authorize(Roles = "Guest")]
        [HttpDelete("{reviewId}")]
        public IActionResult DeleteReview(int reviewId)
        {
            var guestIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (guestIdClaim == null)
            {
                return Unauthorized();
            }
            var review = _reviewService.GetReviewById(reviewId);
            if (review == null || review.GuestId != int.Parse(guestIdClaim))
            {
                return NotFound();
            }

            _reviewService.DeleteReview(reviewId);
            return Ok("Review Deleted");
        }

        [Authorize(Roles = "Manager, Admin, Receptionist, Guest")]
        [HttpGet("reservation/{reservationId}")]
        public IActionResult GetReviewsByReservationId(int reservationId)
        {
            var reviews = _reviewService.GetReviewsByReservationId(reservationId);
            return Ok(reviews);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpGet("all-reviews")]
        public IActionResult GetAllReviews()
        {
            var reviews = _reviewService.GetAllReviews();
            return Ok(reviews);
        }
    }
}

