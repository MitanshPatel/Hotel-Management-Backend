using Hostel_Management.Data;
using Hostel_Management.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hostel_Management.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Review> GetAllReviews()
        {
            return _context.Reviews.ToList();
        }

        public IEnumerable<Review> GetReviewsByGuestId(int guestId)
        {
            return _context.Reviews.Where(r => r.GuestId == guestId).ToList();
        }

        public IEnumerable<Review> GetReviewsByReservationId(int reservationId)
        {
            return _context.Reviews.Where(r => r.ReservationId == reservationId).ToList();
        }

        public Review GetReviewById(int reviewId)
        {
            return _context.Reviews.Find(reviewId);
        }

        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
        }

        public void UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
        }

        public void DeleteReview(int reviewId)
        {
            var review = _context.Reviews.Find(reviewId);
            if (review != null)
            {
                _context.Reviews.Remove(review);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

