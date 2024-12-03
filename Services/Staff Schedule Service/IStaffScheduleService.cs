using Hostel_Management.Models;

namespace Hostel_Management.Services
{
    public interface IStaffScheduleService
    {
        IEnumerable<StaffSchedule> GetAllSchedules();
        IEnumerable<StaffSchedule> GetSchedulesByStaffId(int staffId);
        void AddSchedule(StaffSchedule schedule);
        void UpdateSchedule(StaffSchedule schedule);
        void AddLeaveEntries(int staffId, DateTime startDate, DateTime endDate);
    }
}

