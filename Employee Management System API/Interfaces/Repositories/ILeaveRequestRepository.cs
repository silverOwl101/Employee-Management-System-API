using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Queries.LeaveRequest;

namespace Employee_Management_System_API.Interfaces.Repositories
{
    public interface ILeaveRequestRepository
    {
        Task<IEnumerable<LeaveRequest>> GetAllAsync(QueryGetAllLeaveRequest query);
        Task<LeaveRequest?> GetByIdAsync(string id);
        Task<LeaveRequest> CreateAsync(LeaveRequest leaveRequest);
        Task<LeaveRequest?> UpdateAsync(Guid id, LeaveRequest leaveRequest);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> IsExistsAsync(Guid id);
    }
}
