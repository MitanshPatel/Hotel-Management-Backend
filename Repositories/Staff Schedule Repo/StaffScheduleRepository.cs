using Hostel_Management.Data;
using Hostel_Management.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hostel_Management.Repositories
{
    public class StaffScheduleRepository : IStaffScheduleRepository
    {
        private readonly ApplicationDbContext _context;

        public StaffScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<StaffSchedule> GetAllSchedules()
        {
            return _context.StaffSchedules.ToList();
        }

        public IEnumerable<StaffSchedule> GetSchedulesByStaffId(int staffId)
        {
            return _context.StaffSchedules.Where(s => s.StaffId == staffId).ToList();
        }

        public StaffSchedule GetScheduleById(int scheduleId)
        {
            return _context.StaffSchedules.Find(scheduleId);
        }

        public void AddSchedule(StaffSchedule schedule)
        {
            _context.StaffSchedules.Add(schedule);
        }

        public void UpdateSchedule(StaffSchedule schedule)
        {
            _context.StaffSchedules.Update(schedule);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
