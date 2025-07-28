using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Queries.Employee;

namespace Employee_Management_System_API.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(QueryGetAllEmployees employeeQuery);
        Task<Employee?> GetPhoneNumbersAsync(QueryGetPhoneNumbersAsync employeeQuery);
        Task<Employee?> GetAttendancesAsync(QueryGetEmployeeAttendance employeeQuery);
        Task<Employee?> GetLeaveRequestsAsync(QueryGetEmployeeLeaveRequest employeeQuery);
        Task<Employee?> GetPayrollsAsync(QueryGetEmployeePayroll employeeQuery);
        Task<Employee?> GetProjectAssignmentsAsync(QueryGetProjectAssignmentsAsync employeeQuery);
        Task<Employee?> GetPerformanceReviewsAsync(QueryGetPerformanceReviewsAsync employeeQuery);
        Task<Employee?> GetByIdAsync(string id);        
        Task<Employee?> GetByGuidAsync(Guid id);
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee?> UpdateAsync(Guid id, Employee employee);
        Task<bool> DeleteAsync(string id);
        Task<bool> isExistsAsync(string id);
    }
}
