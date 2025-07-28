using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Queries.Payroll;

namespace Employee_Management_System_API.Interfaces.Services
{
    public interface IPayrollService
    {
        Task<IEnumerable<PayrollResponse>> GetAllPayrollAsync(QueryGetAllPayroll query);
        Task<PayrollResponse?> GetPayrollByIdAsync(string id);
        Task<PayrollResponse> CreatePayrollAsync(UpsertPayrollRequest payroll);
        Task<PayrollResponse?> UpdatePayrollAsync(string id, UpsertPayrollRequest payroll);
        Task<bool> DeletePayrollAsync(string id);
        Task<bool> IsPayrollExistsAsync(Guid id);
    }
}
