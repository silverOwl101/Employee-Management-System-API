using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Helpers.DataManipulators;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Queries.Attendance;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_API.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDBContext _context;
        public AttendanceRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Attendance> CreateAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
            return await Task.FromResult(attendance);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var attendanceExist = await _context.Attendances.FirstOrDefaultAsync(e => e.AttendanceUID == id);
            if (attendanceExist != null)
                _context.Attendances.Remove(attendanceExist);
            return await _context.SaveChangesAsync() > -1 ? true : false;
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync(QueryGetAllAttendance query)
        {
            var attendance = _context.Attendances.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.AttendancePub_ID))
                attendance = attendance.Where(q => q.AttendancePub_ID == query.AttendancePub_ID);

            if (query.Date.HasValue)
                attendance = attendance.Where(q => q.Date == query.Date);

            if (query.CheckInTime.HasValue)
                attendance = attendance.Where(q => q.CheckInTime == query.CheckInTime);

            if (query.CheckOutTime.HasValue)
                attendance = attendance.Where(q => q.CheckOutTime == query.CheckOutTime);

            if (query.Status.HasValue)
                attendance = attendance.Where(q => q.Status == query.Status);

            if (query.Sortby.HasValue)
                attendance = EmployeeSorters.Sort(attendance, query.Sortby.ToString() ?? "", query.IsDecsending).AsQueryable();

            return await EmployeePagination.Pagination(attendance, query.PageNumber, query.PageSize).ToListAsync();
        }

        public async Task<Attendance?> GetByIdAsync(string id)
        {
            return await _context.Attendances.FirstOrDefaultAsync(e => e.AttendancePub_ID == id);
        }

        public async Task<bool> IsExistsAsync(DateOnly date)
        {
            return await _context.Attendances.AnyAsync(e => e.Date == date);
        }

        public async Task<Attendance?> UpdateAsync(Guid id, Attendance attendance)
        {
            var existingAttendance = await _context.Attendances.FirstOrDefaultAsync(e => e.AttendanceUID == id);
            if (existingAttendance != null)
            {
                existingAttendance.AttendancePub_ID = attendance.AttendancePub_ID;
                existingAttendance.Date = attendance.Date;
                existingAttendance.CheckInTime = attendance.CheckInTime;
                existingAttendance.CheckOutTime = attendance.CheckOutTime;
                existingAttendance.Status = attendance.Status;
                _context.Attendances.Update(existingAttendance);
                await _context.SaveChangesAsync();
                return existingAttendance;
            }
            return null;
        }
    }
}
