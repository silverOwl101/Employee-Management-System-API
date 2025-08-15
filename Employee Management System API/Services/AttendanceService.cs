using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Helpers;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Mappings;
using Employee_Management_System_API.Queries.Attendance;

namespace Employee_Management_System_API.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepo;
        private readonly IEmployeeRepository _employeeRepo;
        public AttendanceService(IAttendanceRepository attendanceRepo, IEmployeeRepository employeeRepo)
        {
            _attendanceRepo = attendanceRepo;
            _employeeRepo = employeeRepo;
        }
        public async Task<AttendanceResponse> CreateAttendanceAsync(UpsertAttendanceRequest attendance)
        {
            var existingEmployee = await _employeeRepo.GetByIdAsync(attendance.EmployeePub_ID);
            if (existingEmployee != null)
            {
                if (!ValidationHelper.isRegexMatch(attendance.AttendancePub_ID))
                    throw new InvalidOperationException($"Attendance ID must be in the format 0000-0000 using only digits.");
                var newAttendance = new Attendance
                {
                    AttendancePub_ID = attendance.AttendancePub_ID,
                    Date = attendance.Date,
                    CheckInTime = attendance.CheckInTime,
                    CheckOutTime = attendance.CheckOutTime,
                    Status = attendance.Status,
                    EmployeeUID = existingEmployee.EmployeeUID
                };

                await _attendanceRepo.CreateAsync(newAttendance);
                return new AttendanceResponse
                {
                    AttendancePub_ID = newAttendance.AttendancePub_ID,
                    Date = newAttendance.Date,
                    CheckInTime = newAttendance.CheckInTime,
                    CheckOutTime = newAttendance.CheckOutTime,
                    Status = newAttendance.Status
                };
            }
            throw new KeyNotFoundException("Employee not found!");
        }

        public async Task<bool> DeleteAttendanceAsync(string id)
        {
            var exist = await _attendanceRepo.GetByIdAsync(id);
            var result = exist != null ? await _attendanceRepo.DeleteAsync(exist.AttendanceUID) : false;
            return result;
        }

        public async Task<IEnumerable<AttendanceResponse>> GetAllAttendanceAsync(QueryGetAllAttendance query)
        {
            var list = await _attendanceRepo.GetAllAsync(query);
            return list.Select(e => e.ToAttendanceDto()).ToList();
        }

        public async Task<AttendanceResponse?> GetAttendanceByIdAsync(string id)
        {
            var existingAttendance = await _attendanceRepo.GetByIdAsync(id);
            if (existingAttendance != null)
                return existingAttendance.ToAttendanceDto();
            throw new KeyNotFoundException("No records found.");
        }

        public async Task<bool> IsAttendanceExistsAsync(DateOnly date)
        {
            return await _attendanceRepo.IsExistsAsync(date);
        }

        public async Task<AttendanceResponse?> UpdateAttendanceAsync(string id, UpsertAttendanceRequest attendance)
        {
            var existingAttendance = await _attendanceRepo.GetByIdAsync(id);
            if (existingAttendance != null)
            {
                var updated = new Attendance
                {
                    AttendancePub_ID = attendance.AttendancePub_ID,
                    Date = attendance.Date,
                    CheckInTime = attendance.CheckInTime,
                    CheckOutTime = attendance.CheckOutTime,
                    Status = attendance.Status
                };
                var newAttendanceValue = await _attendanceRepo.UpdateAsync(existingAttendance.AttendanceUID, updated);
                if (newAttendanceValue != null)
                    return newAttendanceValue.ToAttendanceDto();
                throw new InvalidOperationException("Attendance cannot be updated!");
            }
            throw new InvalidOperationException("Attendance cannot be updated!");
        }
    }
}
