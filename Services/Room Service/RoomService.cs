using Hostel_Management.Models;
using Hostel_Management.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Hostel_Management.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationRepository _reservationRepository;

        public RoomService(IRoomRepository roomRepository, IReservationRepository reservationRepository)
        {
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _roomRepository.GetAllRooms();
        }

        public Room GetRoomById(int roomId)
        {
            return _roomRepository.GetRoomById(roomId);
        }

        public void AddRoom(Room room)
        {
            _roomRepository.AddRoom(room);
            _roomRepository.SaveChanges();
        }

        public void UpdateRoom(Room room)
        {
            _roomRepository.UpdateRoom(room);
            _roomRepository.SaveChanges();
        }

        public void DeleteRoom(int roomId)
        {
            _roomRepository.DeleteRoom(roomId);
            _roomRepository.SaveChanges();
        }

        
    }
}

