using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Response;

namespace Employee_Management_System_API.Mappings
{
    public static class LeaveRequestMappers
    {
        public static LeaveRequestResponse ToLeaveRequestDTO(this LeaveRequest leaveRequest)
        {
            return new LeaveRequestResponse
            {
                LeavePub_ID = leaveRequest.LeavePub_ID,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                LeaveType = leaveRequest.LeaveType,
                Status = leaveRequest.Status,
                Reason = leaveRequest.Reason
            };
        }
    }
}
