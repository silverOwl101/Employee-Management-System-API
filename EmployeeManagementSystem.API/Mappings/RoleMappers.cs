using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Response;

namespace Employee_Management_System_API.Mappings
{
    public static class RoleMappers
    {
        public static RoleResponse ToRoleDto(this Role role)
        {
            return new RoleResponse
            {
                RolePub_ID = role.RolePub_ID,
                RoleName = role.RoleName,
                Description = role.Description,
                Salary = role.Salary
            };
        }
    }
}
