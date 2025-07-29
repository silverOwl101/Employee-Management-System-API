using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Queries.Employee;

namespace Employee_Management_System_API.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeResponse>> GetAllEmployeeAsync(QueryGetAllEmployees employees);
        Task<EmployeePhoneNumberResponse> GetEmployeePhoneNumbersAsync(string id, QueryGetPhoneNumbersAsync query);
        Task<EmployeeAttendanceResponse> GetEmployeeAttendancesAsync(string id, QueryGetEmployeeAttendance query);
        Task<EmployeeLeaveResponse> GetEmployeeLeaveRequestsAsync(string id, QueryGetEmployeeLeaveRequest query);
        Task<EmployeePayrollResponse> GetEmployeePayrollsAsync(string id, QueryGetEmployeePayroll query);
        Task<EmployeeProjectAssignmentResponse> GetEmployeeProjectAssignmentsAsync(string id, QueryGetProjectAssignmentsAsync query);
        Task<EmployeePerformanceReviewResponse> GetEmployeePerformanceReviewsAsync(string id, QueryGetPerformanceReviewsAsync query);
        Task<EmployeeResponse?> GetEmployeeByIdAsync(string id);
        Task<EmployeeResponse?> GetEmployeeByGuidAsync(Guid id);
        Task<EmployeeResponse> CreateEmployeeAsync(UpsertEmployeeRequest employee);
        Task<EmployeeResponse?> UpdateEmployeeAsync(string id, UpsertEmployeeRequest employee);
        Task<bool> DeleteEmployeeAsync(string id);
        Task<bool> isEmployeeExistsAsync(string id);
    }
}
