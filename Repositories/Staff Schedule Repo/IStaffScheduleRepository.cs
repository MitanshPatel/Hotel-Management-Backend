using Hostel_Management.Models;
using System.Collections.Generic;

namespace Hostel_Management.Repositories
{
    public interface IStaffScheduleRepository
    {
        IEnumerable<StaffSchedule> GetAllSchedules();
        IEnumerable<StaffSchedule> GetSchedulesByStaffId(int staffId);
        StaffSchedule GetScheduleById(int scheduleId);
        void AddSchedule(StaffSchedule schedule);
        void UpdateSchedule(StaffSchedule schedule);
        void SaveChanges();
    }
}
