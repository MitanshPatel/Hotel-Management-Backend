using Hostel_Management.Models;
using Hostel_Management.Repositories;
using System.Collections.Generic;

namespace Hostel_Management.Services
{
    public class StaffShiftService : IStaffShiftService
    {
        private readonly IStaffShiftRepository _staffShiftRepository;

        public StaffShiftService(IStaffShiftRepository staffShiftRepository)
        {
            _staffShiftRepository = staffShiftRepository;
        }

        public IEnumerable<StaffShift> GetAllShifts()
        {
            return _staffShiftRepository.GetAllShifts();
        }

        public StaffShift GetShiftByStaffId(int staffId)
        {
            return _staffShiftRepository.GetShiftByStaffId(staffId);
        }

        public void AddShift(StaffShift shift)
        {
            _staffShiftRepository.AddShift(shift);
            _staffShiftRepository.SaveChanges();
        }

        public void UpdateShift(StaffShift shift)
        {
            _staffShiftRepository.UpdateShift(shift);
            _staffShiftRepository.SaveChanges();
        }

        public void DeleteShift(int staffShiftId)
        {
            _staffShiftRepository.DeleteShift(staffShiftId);
            _staffShiftRepository.SaveChanges();
        }
    }
}
