using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Helpers.DataManipulators;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Mappings;
using Employee_Management_System_API.Queries.Department;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_API.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDBContext _context;
        public DepartmentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Department> CreateAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return await Task.FromResult(department);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentUID == id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
                return true; // Department deleted successfully   
            }
            return false; // Department not found            
        }
        
        public async Task<IEnumerable<Department>> GetAllAsync(QueryGetAllDepartment query)
        {
            var department = _context.Departments.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.DepartmentPub_ID))
                department = department.Where(q => q.DepartmentPub_ID == query.DepartmentPub_ID);

            if (!string.IsNullOrEmpty(query.DepartmentName))
                department = department.Where(q => q.DepartmentName == query.DepartmentName);
            
            if (query.Sortby.HasValue)
                department = EmployeeSorters.Sort(department, query.Sortby.ToString() ?? "", query.IsDecsending).AsQueryable();

            return await EmployeePagination.Pagination(department, query.PageNumber, query.PageSize).ToListAsync();
        }

        public async Task<IEnumerable<Department?>> GetAllByGuidAsync(List<Guid> guids)
        {
            return await _context.Departments.Where(d => guids.Contains(d.DepartmentUID)).ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(string id)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentPub_ID == id);         
        }

        public async Task<bool> IsIdExistsAsync(string id)
        {
            return await _context.Departments.AnyAsync(d => d.DepartmentPub_ID == id);
        }

        public async Task<bool> IsNameExistsAsync(string name)
        {
            return await _context.Departments.AnyAsync(d => d.DepartmentName == name);
        }

        public async Task<Department?> UpdateAsync(Guid id, Department _department)
        {            
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentUID == id);
            if (department != null)
            {                                
                department.DepartmentPub_ID = _department.DepartmentPub_ID;
                department.DepartmentName = _department.DepartmentName;
                department.Description = _department.Description;
                _context.Departments.Update(department);
                await _context.SaveChangesAsync();
                return department; // Return the updated department
            }
            return null; // Department not found
        }        
    }
}
