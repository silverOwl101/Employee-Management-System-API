using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Helpers;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Mappings;
using Employee_Management_System_API.Queries.Role;

namespace Employee_Management_System_API.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repo;
        public RoleService(IRoleRepository repo)
        {
            _repo = repo;
        }
        public async Task<RoleResponse> CreateRoleAsync(UpsetRoleRequest role)
        {
            if (await _repo.IsIdExistsAsync(role.RolePub_ID) ||
                await _repo.IsNameExistsAsync(role.RoleName))
                throw new InvalidOperationException($"Role existed");

            if (!ValidationHelper.isRegexMatch(role.RolePub_ID))
                throw new InvalidOperationException($"Role ID must be in the format 0000-0000 using only digits.");
            var roleEntity = new Role
            {
                RolePub_ID = role.RolePub_ID,
                RoleName = role.RoleName,
                Description = role.Description,
                Salary = role.Salary
            };
            await _repo.CreateAsync(roleEntity);

            return new RoleResponse
            {
                RolePub_ID = roleEntity.RolePub_ID,
                RoleName = roleEntity.RoleName,
                Description = roleEntity.Description,
                Salary = roleEntity.Salary
            };
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            var existingRole = await _repo.GetByIdAsync(id);
            if (existingRole == null)
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            return await _repo.DeleteAsync(existingRole.RoleUID);
        }

        public async Task<IEnumerable<RoleResponse>> GetAllRolesAsync(QueryGetAllRole query)
        {
            var list = await _repo.GetAllAsync(query);
            return list.Select(e => e.ToRoleDto()).ToList();
        }

        public async Task<RoleResponse?> GetRoleByIdAsync(string id)
        {
            var role = await _repo.GetByIdAsync(id);
            if (role is not null)
                return role.ToRoleDto();
            throw new KeyNotFoundException($"Role ID is required.");
        }

        public async Task<RoleResponse?> UpdateRoleAsync(string id, UpsetRoleRequest role)
        {
            var existingRole = await _repo.GetByIdAsync(id);
            if (existingRole != null)
            {
                if (await _repo.IsIdExistsAsync(role.RolePub_ID) && role.RolePub_ID != existingRole.RolePub_ID)
                    throw new InvalidOperationException($"Role ID already exists.");
                if (await _repo.IsNameExistsAsync(role.RoleName) && role.RoleName != existingRole.RoleName)
                    throw new InvalidOperationException($"Role name already exists.");

                var updatedRole = new Role
                {
                    RolePub_ID = role.RolePub_ID,
                    RoleName = role.RoleName,
                    Description = role.Description,
                    Salary = role.Salary
                };
                var result = await _repo.UpdateAsync(existingRole.RoleUID, updatedRole);
                return result != null ? new RoleResponse
                {
                    RolePub_ID = result.RolePub_ID,
                    RoleName = result.RoleName,
                    Description = result.Description,
                    Salary = result.Salary
                } : throw new InvalidOperationException($"Role with ID {id} could not be updated."); ;
            }
            else
            {
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            }
        }
    }
}
