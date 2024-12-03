﻿using Hostel_Management.Models;
using System.Collections.Generic;

namespace Hostel_Management.Repositories
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetAllReviews();
        IEnumerable<Review> GetReviewsByGuestId(int guestId);
        IEnumerable<Review> GetReviewsByReservationId(int reservationId);
        Review GetReviewById(int reviewId);
        void AddReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(int reviewId);
        void SaveChanges();
    }
}
