using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Helpers;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Mappings;
using Employee_Management_System_API.Queries.LeaveRequest;

namespace Employee_Management_System_API.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepo;
        private readonly IEmployeeRepository _employeeRepo;
        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepo, IEmployeeRepository employeeRepo)
        {
            _leaveRequestRepo = leaveRequestRepo;
            _employeeRepo = employeeRepo;
        }
        public async Task<LeaveRequestResponse> CreateLeaveRequestAsync(UpsertLeaveRequest_Request leaveRequest)
        {
            var existingEmployee = await _employeeRepo.GetByIdAsync(leaveRequest.EmployeePub_ID);
            if (existingEmployee != null)
            {
                if (!ValidationHelper.isRegexMatch(leaveRequest.LeavePub_ID))
                    throw new InvalidOperationException($"Leave request ID must be in the format 0000-0000 using only digits.");
                var initLeaveRequest = new LeaveRequest
                {
                    LeavePub_ID = leaveRequest.LeavePub_ID,
                    StartDate = leaveRequest.StartDate,
                    EndDate = leaveRequest.EndDate,
                    LeaveType = leaveRequest.LeaveType,
                    Status = leaveRequest.Status,
                    Reason = leaveRequest.Reason,
                    EmployeeUID = existingEmployee.EmployeeUID
                };

                var createdLeaveRequest = await _leaveRequestRepo.CreateAsync(initLeaveRequest);
                return createdLeaveRequest.ToLeaveRequestDTO();
            }
            throw new KeyNotFoundException("Employee not found!");
        }

        public async Task<bool> DeleteLeaveRequestAsync(string id)
        {
            var existing = await _leaveRequestRepo.GetByIdAsync(id);
            var result = existing != null ? await _leaveRequestRepo.DeleteAsync(existing.LeaveUID) : false;
            return result;
        }

        public async Task<IEnumerable<LeaveRequestResponse>> GetAllLeaveRequestAsync(QueryGetAllLeaveRequest query)
        {
            var list = await _leaveRequestRepo.GetAllAsync(query);
            return list.Select(e => e.ToLeaveRequestDTO()).ToList();
        }

        public async Task<LeaveRequestResponse?> GetLeaveRequestByIdAsync(string id)
        {
            var exist = await _leaveRequestRepo.GetByIdAsync(id);
            if (exist != null)
                return exist.ToLeaveRequestDTO();
            throw new KeyNotFoundException("No records found.");
        }

        public async Task<bool> IsLeaveRequestExistsAsync(Guid id)
        {
            return await _leaveRequestRepo.IsExistsAsync(id);
        }

        public async Task<LeaveRequestResponse?> UpdateLeaveRequestAsync(string id, UpsertLeaveRequest_Request leaveRequest)
        {
            var existing = await _leaveRequestRepo.GetByIdAsync(id);
            if (existing != null)
            {
                var updated = new LeaveRequest
                {
                    LeavePub_ID = leaveRequest.LeavePub_ID,
                    StartDate = leaveRequest.StartDate,
                    EndDate = leaveRequest.EndDate,
                    LeaveType = leaveRequest.LeaveType,
                    Status = leaveRequest.Status,
                    Reason = leaveRequest.Reason
                };
                var newLeaveRequest = await _leaveRequestRepo.UpdateAsync(existing.LeaveUID, updated);
                if (newLeaveRequest != null)
                    return newLeaveRequest.ToLeaveRequestDTO();
                throw new InvalidOperationException("Leave request cannot be updated!");
            }
            throw new InvalidOperationException("Leave request cannot be updated!");
        }
    }
}
