using Hostel_Management.Data;
using Hostel_Management.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hostel_Management.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _context.Rooms.ToList();
        }

        public Room GetRoomById(int roomId)
        {
            return _context.Rooms.SingleOrDefault(r => r.RoomId == roomId);
        }

        public void AddRoom(Room room)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Rooms ON");
                _context.Rooms.Add(room);
                _context.SaveChanges();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Rooms OFF");
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void UpdateRoom(Room room)
        {
            _context.Rooms.Update(room);
        }

        public void DeleteRoom(int roomId)
        {
            var room = _context.Rooms.SingleOrDefault(r => r.RoomId == roomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

