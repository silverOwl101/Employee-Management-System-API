using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Queries.Department;

namespace Employee_Management_System_API.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync(QueryGetAllDepartment query);
        Task<IEnumerable<Department?>> GetAllByGuidAsync(List<Guid> guids);
        Task<Department?> GetByIdAsync(string id);
        Task<Department> CreateAsync(Department department);
        Task<Department?> UpdateAsync(Guid id, Department department);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> IsIdExistsAsync(string id);
        Task<bool> IsNameExistsAsync(string id);

    }
}
