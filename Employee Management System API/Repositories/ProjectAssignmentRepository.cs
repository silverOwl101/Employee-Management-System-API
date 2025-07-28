using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Helpers.DataManipulators;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Queries.ProjectAssignment;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_API.Repositories
{
    public class ProjectAssignmentRepository : IProjectAssignmentRepository
    {
        private readonly ApplicationDBContext _context;
        public ProjectAssignmentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<ProjectAssignment> CreateAsync(ProjectAssignment projectAssignment)
        {
            await _context.EmployeeProjectAssignments.AddAsync(projectAssignment);
            await _context.SaveChangesAsync();
            return await Task.FromResult(projectAssignment);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var exist = await _context.EmployeeProjectAssignments.FirstOrDefaultAsync(e => e.AssignmentUID == id);
            if (exist != null)
                _context.EmployeeProjectAssignments.Remove(exist);
            return await _context.SaveChangesAsync() > -1 ? true : false;
        }

        public async Task<IEnumerable<ProjectAssignment>> GetAllAsync(QueryGetAllProjectAssignment query)
        {
            var employeeProjectAssignment = _context.EmployeeProjectAssignments.Include(e => e.Employee)
                                                                               .Include(e => e.Project)
                                                                               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.AssignmentPub_ID))
                employeeProjectAssignment = employeeProjectAssignment.Where(q => q.AssignmentPub_ID == query.AssignmentPub_ID);

            if (!string.IsNullOrEmpty(query.RoleInProject))
                employeeProjectAssignment = employeeProjectAssignment.Where(q => q.RoleInProject == query.RoleInProject);

            if (query.AssignedDate.HasValue)
                employeeProjectAssignment = employeeProjectAssignment.Where(q => q.AssignedDate == query.AssignedDate);
            
            if (query.Sortby.HasValue)
                employeeProjectAssignment = EmployeeSorters.Sort(employeeProjectAssignment, query.Sortby.ToString() ?? "", query.IsDecsending).AsQueryable();

            return await EmployeePagination.Pagination(employeeProjectAssignment, query.PageNumber, query.PageSize).ToListAsync();            
        }

        public async Task<IEnumerable<ProjectAssignment>> GetAssignedEmployeesAsync()
        {
            return await _context.EmployeeProjectAssignments.Include(e => e.Employee)
                                                            .Include(e => e.Project)
                                                            .ToListAsync();
        }

        public async Task<ProjectAssignment?> GetByIdAsync(string id)
        {
            return await _context.EmployeeProjectAssignments.Include(e => e.Employee)
                                                            .Include(e => e.Project)
                                                            .FirstOrDefaultAsync(e => e.AssignmentPub_ID == id);
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.EmployeeProjectAssignments.AnyAsync(e => e.AssignmentUID == id);
        }

        public async Task<ProjectAssignment?> UpdateAsync(Guid id, ProjectAssignment projectAssignment)
        {
            var existingAssignment = await _context.EmployeeProjectAssignments.FirstOrDefaultAsync(e => e.AssignmentUID == id);
            if (existingAssignment != null)
            {
                existingAssignment.AssignmentPub_ID = projectAssignment.AssignmentPub_ID;
                existingAssignment.RoleInProject = projectAssignment.RoleInProject;
                existingAssignment.AssignedDate = projectAssignment.AssignedDate;
                _context.EmployeeProjectAssignments.Update(existingAssignment);
                await _context.SaveChangesAsync();
                return existingAssignment;
            }
            return null;
        }
    }
}
