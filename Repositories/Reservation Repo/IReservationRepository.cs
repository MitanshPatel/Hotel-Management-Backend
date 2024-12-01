using Hostel_Management.Models;

public interface IReservationRepository
{
    IEnumerable<Reservation> GetAllReservations();
    IEnumerable<Reservation> GetReservationsByStatus(string status);
    Reservation GetReservationById(int reservationId);
    void AddReservation(Reservation reservation);
    void UpdateReservation(Reservation reservation);
    void DeleteReservation(int reservationId);
    void SaveChanges();
    IEnumerable<Reservation> GetReservationsByGuestId(int guestId);
}
