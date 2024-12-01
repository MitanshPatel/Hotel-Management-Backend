using Hostel_Management.Models;
using Hostel_Management.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Hostel_Management.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;

        public ReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetAllReservations();
        }
        public IEnumerable<Reservation> GetPendingReservations()
        {
            return _reservationRepository.GetReservationsByStatus("Pending");
        }
        public IEnumerable<Reservation> GetApprovedReservations()
        {
            return _reservationRepository.GetReservationsByStatus("Approved");
        }

        public IEnumerable<Reservation> GetRejectedReservations()
        {
            return _reservationRepository.GetReservationsByStatus("Rejected");
        }

        public Reservation GetReservationById(int reservationId)
        {
            return _reservationRepository.GetReservationById(reservationId);
        }

        public void AddReservation(Reservation reservation)
        {
            _reservationRepository.AddReservation(reservation);
            _reservationRepository.SaveChanges();
        }

        public void UpdateReservation(Reservation reservation)
        {
            _reservationRepository.UpdateReservation(reservation);
            _reservationRepository.SaveChanges();
        }

        public void DeleteReservation(int reservationId)
        {
            _reservationRepository.DeleteReservation(reservationId);
            _reservationRepository.SaveChanges();
        }

        public bool BookRoom(Reservation reservation)
        {
            var room = _roomRepository.GetRoomById(reservation.RoomId);
            if (room == null || !room.Availability)
            {
                return false;
            }

            var check = GetNextAvailableTimes(reservation.RoomId, reservation.CheckInDate, reservation.CheckOutDate);

            if (check.isAvailable == false)
            {
                return false;
            }

            _reservationRepository.AddReservation(reservation);
            _reservationRepository.SaveChanges();

            return true;
        }

        public IEnumerable<Reservation> GetReservationsByGuestId(int guestId)
        {
            return _reservationRepository.GetReservationsByGuestId(guestId);
        }

        public (bool isAvailable, DateTime? nextAvailableCheckIn, DateTime? nextAvailableCheckOut) GetNextAvailableTimes(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            var availableRooms = GetAvailableRooms(checkInDate, checkOutDate);
            if (!availableRooms.Any(r => r.RoomId == roomId))
            {
                return (false, null, null);
            }

            var reservations = _reservationRepository.GetAllReservations()
                .Where(r => r.RoomId == roomId)
                .OrderBy(r => r.CheckInDate)
                .ToList();

            bool flag = false;
            DateTime? nextAvailableCheckIn = null;
            DateTime? nextAvailableCheckOut = null;

            foreach (var reservation in reservations)
            {
                if (checkInDate.Date == reservation.CheckOutDate.Date)
                {
                    if (checkInDate < reservation.CheckOutDate.AddHours(3))
                    {
                        nextAvailableCheckIn = reservation.CheckOutDate.AddHours(3);
                        flag = true;
                    }
                }
                if (checkOutDate.Date == reservation.CheckInDate.Date)
                {
                    if (checkOutDate > reservation.CheckInDate.AddHours(-3))
                    {
                        nextAvailableCheckOut = reservation.CheckInDate.AddHours(-3);
                        flag = true;
                    }
                }
            }
            if (flag) return (false, nextAvailableCheckIn, nextAvailableCheckOut);
            return (true, null, null);
        }


        public IEnumerable<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        {
            var reservedRoomIds = _reservationRepository.GetAllReservations()
                .Where(r => r.CheckInDate.Date < checkOutDate.Date && r.CheckOutDate.Date > checkInDate.Date && (r.Status=="Approved" || r.Status=="Pending"))
                .Select(r => r.RoomId)
                .Distinct()
                .ToList();

            return _roomRepository.GetAllRooms()
                .Where(r => !reservedRoomIds.Contains(r.RoomId) && r.Availability)
                .ToList();
        }

        public void UpdateReservationStatus(int reservationId, string status)
        {
            var reservation = _reservationRepository.GetReservationById(reservationId);
            if (reservation == null)
            {
                throw new Exception("Reservation not found.");
            }

            reservation.Status = status;
            _reservationRepository.UpdateReservation(reservation);
            _reservationRepository.SaveChanges();
        }
    }
}

