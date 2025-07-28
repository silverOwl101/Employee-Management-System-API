using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Queries.Attendance;

namespace Employee_Management_System_API.Interfaces.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceResponse>> GetAllAttendanceAsync(QueryGetAllAttendance query);
        Task<AttendanceResponse?> GetAttendanceByIdAsync(string id);
        Task<AttendanceResponse> CreateAttendanceAsync(UpsertAttendanceRequest attendance);
        Task<AttendanceResponse?> UpdateAttendanceAsync(string id, UpsertAttendanceRequest attendance);
        Task<bool> DeleteAttendanceAsync(string id);
        Task<bool> IsAttendanceExistsAsync(DateOnly date);
    }
}
