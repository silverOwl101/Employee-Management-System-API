using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Queries.LeaveRequest;

namespace Employee_Management_System_API.Interfaces.Services
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<LeaveRequestResponse>> GetAllLeaveRequestAsync(QueryGetAllLeaveRequest query);
        Task<LeaveRequestResponse?> GetLeaveRequestByIdAsync(string id);
        Task<LeaveRequestResponse> CreateLeaveRequestAsync(UpsertLeaveRequest_Request leaveRequest);
        Task<LeaveRequestResponse?> UpdateLeaveRequestAsync(string id, UpsertLeaveRequest_Request leaveRequest);
        Task<bool> DeleteLeaveRequestAsync(string id);
        Task<bool> IsLeaveRequestExistsAsync(Guid id);
    }
}
