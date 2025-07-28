using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Queries.Payroll;

namespace Employee_Management_System_API.Interfaces.Repositories
{
    public interface IPayrollRepository
    {
        Task<IEnumerable<Payroll>> GetAllAsync(QueryGetAllPayroll query);
        Task<Payroll?> GetByIdAsync(string id);
        Task<Payroll> CreateAsync(Payroll payroll);
        Task<Payroll?> UpdateAsync(Guid id, Payroll payroll);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> IsExistsAsync(Guid id);
    }
}
