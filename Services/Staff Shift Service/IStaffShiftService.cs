using Hostel_Management.Models;
using System.Collections.Generic;

namespace Hostel_Management.Services
{
    public interface IStaffShiftService
    {
        IEnumerable<StaffShift> GetAllShifts();
        StaffShift GetShiftByStaffId(int staffId);
        void AddShift(StaffShift shift);
        void UpdateShift(StaffShift shift);
        void DeleteShift(int staffShiftId);
    }
}
