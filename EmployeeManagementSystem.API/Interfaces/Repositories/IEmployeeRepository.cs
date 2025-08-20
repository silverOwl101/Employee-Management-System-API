using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Queries.Employee;

namespace Employee_Management_System_API.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(QueryGetAllEmployees employeeQuery);
        Task<Employee?> GetPhoneNumbersAsync(string id, QueryGetPhoneNumbersAsync employeeQuery);
        Task<Employee?> GetAttendancesAsync(string id, QueryGetEmployeeAttendance employeeQuery);
        Task<Employee?> GetLeaveRequestsAsync(string id, QueryGetEmployeeLeaveRequest employeeQuery);
        Task<Employee?> GetPayrollsAsync(string id, QueryGetEmployeePayroll employeeQuery);
        Task<Employee?> GetProjectAssignmentsAsync(string id, QueryGetProjectAssignmentsAsync employeeQuery);
        Task<Employee?> GetPerformanceReviewsAsync(string id, QueryGetPerformanceReviewsAsync employeeQuery);
        Task<Employee?> GetByIdAsync(string id);        
        Task<Employee?> GetByGuidAsync(Guid id);
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee?> UpdateAsync(Guid id, Employee employee);
        Task<bool> DeleteAsync(string id);
        Task<bool> isExistsAsync(string id);
    }
}
