using Hostel_Management.Models;
using System.Collections.Generic;

namespace Hostel_Management.Repositories
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAllReservations();
        Reservation GetReservationById(int reservationId);
        void AddReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation);
        void DeleteReservation(int reservationId);
        void SaveChanges();
        IEnumerable<Reservation> GetReservationsByGuestId(int guestId);
    }
}

