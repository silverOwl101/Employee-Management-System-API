using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Queries.Attendance;

namespace Employee_Management_System_API.Interfaces.Repositories
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAsync(QueryGetAllAttendance query);
        Task<Attendance?> GetByIdAsync(string id);
        Task<Attendance> CreateAsync(Attendance attendance);
        Task<Attendance?> UpdateAsync(Guid id, Attendance attendance);
        Task<bool> DeleteAsync(Guid id);        
        Task<bool> IsExistsAsync(DateOnly date);
    }
}
