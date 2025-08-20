using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Helpers.DataManipulators;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Queries.Role;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDBContext _context;
        public RoleRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Role> CreateAsync(Role role)
        {
            await _context.EmployeeRoles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<bool> DeleteAsync(Guid guid)
        {
            var role = await _context.EmployeeRoles.FirstOrDefaultAsync(r => r.RoleUID == guid);
            if (role != null)
            {
                _context.EmployeeRoles.Remove(role);
                return await _context.SaveChangesAsync() > 0; // Role deleted successfully
            }
            return false; // Role not found
        }

        public async Task<IEnumerable<Role>> GetAllAsync(QueryGetAllRole query)
        {
            var role = _context.EmployeeRoles.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.RolePub_ID))
                role = role.Where(q => q.RolePub_ID == query.RolePub_ID);

            if (!string.IsNullOrEmpty(query.RoleName))
                role = role.Where(q => q.RoleName == query.RoleName);

            if (query.Sortby.HasValue)
                role = EmployeeSorters.Sort(role, query.Sortby.ToString() ?? "", query.IsDecsending).AsQueryable();

            return await EmployeePagination.Pagination(role, query.PageNumber, query.PageSize).ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(string id)
        {
            return await _context.EmployeeRoles.FirstOrDefaultAsync(r => r.RolePub_ID.ToString() == id);
        }

        public async Task<bool> IsIdExistsAsync(string id)
        {
            return await _context.EmployeeRoles.AnyAsync(r => r.RolePub_ID.ToString() == id);
        }

        public async Task<bool> IsNameExistsAsync(string name)
        {
            return await _context.EmployeeRoles.AnyAsync(r => r.RoleName == name);
        }

        public async Task<Role?> UpdateAsync(Guid id, Role _role)
        {
            var role = await _context.EmployeeRoles.FirstOrDefaultAsync(r => r.RoleUID == id);
            if (role != null)
            {
                role.RolePub_ID = _role.RolePub_ID;
                role.RoleName = _role.RoleName;
                role.Description = _role.Description;
                role.Salary = _role.Salary;
                _context.EmployeeRoles.Update(role);
                await _context.SaveChangesAsync();
                return role;
            }
            return null;
        }
    }
}
