using Hostel_Management.Models;
using Hostel_Management.Repositories;
using System.Collections.Generic;

namespace Hostel_Management.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public IEnumerable<Review> GetAllReviews()
        {
            return _reviewRepository.GetAllReviews();
        }

        public IEnumerable<Review> GetReviewsByGuestId(int guestId)
        {
            return _reviewRepository.GetReviewsByGuestId(guestId);
        }

        public IEnumerable<Review> GetReviewsByReservationId(int reservationId)
        {
            return _reviewRepository.GetReviewsByReservationId(reservationId);
        }

        public Review GetReviewById(int reviewId)
        {
            return _reviewRepository.GetReviewById(reviewId);
        }

        public void AddReview(Review review)
        {
            _reviewRepository.AddReview(review);
            _reviewRepository.SaveChanges();
        }

        public void UpdateReview(Review review)
        {
            _reviewRepository.UpdateReview(review);
            _reviewRepository.SaveChanges();
        }

        public void DeleteReview(int reviewId)
        {
            _reviewRepository.DeleteReview(reviewId);
            _reviewRepository.SaveChanges();
        }
    }
}

