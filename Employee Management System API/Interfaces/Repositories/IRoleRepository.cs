using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Queries.Role;

namespace Employee_Management_System_API.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync(QueryGetAllRole query);
        Task<Role?> GetByIdAsync(string id);
        Task<Role> CreateAsync(Role role);
        Task<Role?> UpdateAsync(Guid id, Role role);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> IsIdExistsAsync(string id);
        Task<bool> IsNameExistsAsync(string name);
    }
}
