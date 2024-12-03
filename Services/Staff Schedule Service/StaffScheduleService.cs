using Hostel_Management.Models;
using Hostel_Management.Repositories;
using System.Collections.Generic;

namespace Hostel_Management.Services
{
    public class StaffScheduleService : IStaffScheduleService
    {
        private readonly IStaffScheduleRepository _staffScheduleRepository;
        private readonly IStaffShiftRepository _staffShiftRepository;

        public StaffScheduleService(IStaffScheduleRepository staffScheduleRepository, IStaffShiftRepository staffShiftRepository)
        {
            _staffScheduleRepository = staffScheduleRepository;
            _staffShiftRepository = staffShiftRepository;
        }

        public IEnumerable<StaffSchedule> GetAllSchedules()
        {
            return _staffScheduleRepository.GetAllSchedules();
        }

        public IEnumerable<StaffSchedule> GetSchedulesByStaffId(int staffId)
        {
            return _staffScheduleRepository.GetSchedulesByStaffId(staffId);
        }

        public void AddSchedule(StaffSchedule schedule)
        {
            _staffScheduleRepository.AddSchedule(schedule);
            _staffScheduleRepository.SaveChanges();
        }

        public void UpdateSchedule(StaffSchedule schedule)
        {
            _staffScheduleRepository.UpdateSchedule(schedule);
            _staffScheduleRepository.SaveChanges();
        }

        public void AddLeaveEntries(int staffId, DateTime startDate, DateTime endDate)
        {
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var schedule = new StaffSchedule
                {
                    StaffId = staffId,
                    ShiftType = null,
                    ShiftStartTime = date,
                    ShiftEndTime = date,
                    Status = "Leave"
                };
                _staffScheduleRepository.AddSchedule(schedule);
            }
            _staffScheduleRepository.SaveChanges();
        }

        public void GenerateDailySchedules()
        {
            var staffShifts = _staffShiftRepository.GetAllShifts();
            foreach (var staffShift in staffShifts)
            {
                var schedule = new StaffSchedule
                {
                    StaffId = staffShift.StaffId,
                    ShiftType = staffShift.ShiftType,
                    ShiftStartTime = DateTime.Today,
                    ShiftEndTime = DateTime.Today.AddHours(8),
                    Status = "Scheduled"
                };
                _staffScheduleRepository.AddSchedule(schedule);
            }
            _staffScheduleRepository.SaveChanges();
        }
    }
}
