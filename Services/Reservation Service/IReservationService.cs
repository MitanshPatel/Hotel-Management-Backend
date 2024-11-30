using Hostel_Management.Models;
using System.Collections.Generic;

namespace Hostel_Management.Services
{
    public interface IReservationService
    {
        IEnumerable<Reservation> GetAllReservations();
        Reservation GetReservationById(int reservationId);
        void AddReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation);
        void DeleteReservation(int reservationId);
        bool BookRoom(Reservation reservation);
        IEnumerable<Reservation> GetReservationsByGuestId(int guestId);
        public (bool isAvailable, DateTime? nextAvailableCheckIn, DateTime? nextAvailableCheckOut) GetNextAvailableTimes(int roomId, DateTime checkInDate, DateTime checkOutDate);
        IEnumerable<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate);
    }
}

