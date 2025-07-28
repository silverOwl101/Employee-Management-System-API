using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Helpers.DataManipulators;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Queries.LeaveRequest;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_API.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDBContext _context;
        public LeaveRequestRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<LeaveRequest> CreateAsync(LeaveRequest leaveRequest)
        {
            await _context.LeaveRequests.AddAsync(leaveRequest);
            await _context.SaveChangesAsync();
            return await Task.FromResult(leaveRequest);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var exist = await _context.LeaveRequests.FirstOrDefaultAsync(e => e.LeaveUID == id);
            if (exist != null)
                _context.LeaveRequests.Remove(exist);
            return await _context.SaveChangesAsync() > -1 ? true : false;
        }

        public async Task<IEnumerable<LeaveRequest>> GetAllAsync(QueryGetAllLeaveRequest query)
        {
            var leaveRequest = _context.LeaveRequests.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.LeavePub_ID))
                leaveRequest = leaveRequest.Where(q => q.LeavePub_ID == query.LeavePub_ID);

            if (query.StartDate.HasValue)
                leaveRequest = leaveRequest.Where(q => q.StartDate == query.StartDate);

            if (query.EndDate.HasValue)
                leaveRequest = leaveRequest.Where(q => q.EndDate == query.EndDate);

            if (query.LeaveType.HasValue)
                leaveRequest = leaveRequest.Where(q => q.LeaveType == query.LeaveType);

            if (query.Status.HasValue)
                leaveRequest = leaveRequest.Where(q => q.Status == query.Status);

            if (query.Sortby.HasValue)
                leaveRequest = EmployeeSorters.Sort(leaveRequest, query.Sortby.ToString() ?? "", query.IsDecsending).AsQueryable();

            return await EmployeePagination.Pagination(leaveRequest, query.PageNumber, query.PageSize).ToListAsync();
        }

        public async Task<LeaveRequest?> GetByIdAsync(string id)
        {
            return await _context.LeaveRequests.FirstOrDefaultAsync(e => e.LeavePub_ID == id);
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.LeaveRequests.AnyAsync(e => e.LeaveUID == id);
        }

        public async Task<LeaveRequest?> UpdateAsync(Guid id, LeaveRequest leaveRequest)
        {
            var existingLeaveRequest = await _context.LeaveRequests.FirstOrDefaultAsync(e => e.LeaveUID == id);
            if (existingLeaveRequest != null)
            {
                existingLeaveRequest.LeavePub_ID = leaveRequest.LeavePub_ID;
                existingLeaveRequest.StartDate = leaveRequest.StartDate;
                existingLeaveRequest.EndDate = leaveRequest.EndDate;
                existingLeaveRequest.LeaveType = leaveRequest.LeaveType;
                existingLeaveRequest.Status = leaveRequest.Status;
                existingLeaveRequest.Reason = leaveRequest.Reason;
                _context.LeaveRequests.Update(existingLeaveRequest);
                await _context.SaveChangesAsync();
                return existingLeaveRequest;
            }
            return null;
        }
    }
}
