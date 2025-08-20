using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Queries.Role;

namespace Employee_Management_System_API.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponse>> GetAllRolesAsync(QueryGetAllRole query);
        Task<RoleResponse?> GetRoleByIdAsync(string id);
        Task<RoleResponse> CreateRoleAsync(UpsetRoleRequest role);
        Task<RoleResponse?> UpdateRoleAsync(string id, UpsetRoleRequest role);
        Task<bool> DeleteRoleAsync(string id);
    }
}
