using Hostel_Management.Data;
using Hostel_Management.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hostel_Management.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _context.Reservations.ToList();
        }

        public Reservation GetReservationById(int reservationId)
        {
            return _context.Reservations.SingleOrDefault(r => r.ReservationId == reservationId);
        }

        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
        }

        public void UpdateReservation(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
        }

        public void DeleteReservation(int reservationId)
        {
            var reservation = _context.Reservations.SingleOrDefault(r => r.ReservationId == reservationId);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Reservation> GetReservationsByGuestId(int guestId)
        {
            return _context.Reservations.Where(r => r.GuestId == guestId).ToList();
        }
    }
}

