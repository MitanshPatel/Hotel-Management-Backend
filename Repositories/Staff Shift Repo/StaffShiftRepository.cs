using Hostel_Management.Data;
using Hostel_Management.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hostel_Management.Repositories
{
    public class StaffShiftRepository : IStaffShiftRepository
    {
        private readonly ApplicationDbContext _context;

        public StaffShiftRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<StaffShift> GetAllShifts()
        {
            return _context.StaffShifts.ToList();
        }

        public StaffShift GetShiftByStaffId(int staffId)
        {
            return _context.StaffShifts.FirstOrDefault(s => s.StaffId == staffId);
        }

        public void AddShift(StaffShift shift)
        {
            _context.StaffShifts.Add(shift);
        }

        public void UpdateShift(StaffShift shift)
        {
            _context.StaffShifts.Update(shift);
        }

        public void DeleteShift(int staffShiftId)
        {
            var shift = _context.StaffShifts.Find(staffShiftId);
            if (shift != null)
            {
                _context.StaffShifts.Remove(shift);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
