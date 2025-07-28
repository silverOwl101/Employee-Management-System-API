using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Helpers.DataManipulators;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Queries.Project;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDBContext _context;
        public ProjectRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Project> CreateAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return await Task.FromResult(project);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var exist = await _context.Projects.FirstOrDefaultAsync(e => e.ProjectUID == id);
            if (exist != null)
                _context.Projects.Remove(exist);
            return await _context.SaveChangesAsync() > -1 ? true : false;
        }

        public async Task<IEnumerable<Project>> GetAllAsync(QueryGetAllProject query)
        {
            var project = _context.Projects.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.ProjectPub_ID))
                project = project.Where(q => q.ProjectPub_ID == query.ProjectPub_ID);

            if (!string.IsNullOrEmpty(query.ProjectName))
                project = project.Where(q => q.ProjectName == query.ProjectName);

            if (query.StartDate.HasValue)
                project = project.Where(q => q.StartDate == query.StartDate);

            if (query.EndDate.HasValue)
                project = project.Where(q => q.EndDate == query.EndDate);

            if (query.Status.HasValue)
                project = project.Where(q => q.Status == query.Status);

            if (query.Sortby.HasValue)
                project = EmployeeSorters.Sort(project, query.Sortby.ToString() ?? "", query.IsDecsending).AsQueryable();

            return await EmployeePagination.Pagination(project, query.PageNumber, query.PageSize).ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(string id, QueryGetById? query)
        {
            var project = await _context.Projects.Include(e => e.EmployeeAssignments)
                                                 .ThenInclude(e => e.Employee)
                                                 .ThenInclude(e => e.Role)
                                                 .AsNoTracking()
                                                 .FirstOrDefaultAsync(e => e.ProjectPub_ID == id);

            IQueryable<ProjectAssignment>? employessAssignedInTheProject;

            if (project is not null && query is not null)
            {
                employessAssignedInTheProject = project.EmployeeAssignments.AsQueryable();

                if (!string.IsNullOrEmpty(query.EmployeePub_ID))
                    employessAssignedInTheProject = employessAssignedInTheProject.Where(q => q.Employee.EmployeePub_ID == query.EmployeePub_ID);

                if (!string.IsNullOrEmpty(query.FirstName))
                    employessAssignedInTheProject = employessAssignedInTheProject.Where(q => q.Employee.FirstName == query.FirstName);

                if (!string.IsNullOrEmpty(query.MiddleName))
                    employessAssignedInTheProject = employessAssignedInTheProject.Where(q => q.Employee.MiddleName == query.MiddleName);

                if (!string.IsNullOrEmpty(query.LastName))
                    employessAssignedInTheProject = employessAssignedInTheProject.Where(q => q.Employee.LastName == query.LastName);

                if (!string.IsNullOrEmpty(query.RoleName))
                    employessAssignedInTheProject = employessAssignedInTheProject.Where(q => q.Employee.Role.RoleName == query.RoleName);

                if (!string.IsNullOrEmpty(query.RoleInProject))
                    employessAssignedInTheProject = employessAssignedInTheProject.Where(q => q.RoleInProject == query.RoleInProject);

                if (!string.IsNullOrEmpty(query.DepartmentName))
                    employessAssignedInTheProject = employessAssignedInTheProject.Where(q => q.Employee.Department.DepartmentName == query.DepartmentName);

                if (query.Status.HasValue)
                    employessAssignedInTheProject = employessAssignedInTheProject.Where(q => q.Employee.Status == query.Status);

                if (query.Sortby.HasValue)
                    employessAssignedInTheProject = EmployeeSorters.Sort(employessAssignedInTheProject, query.Sortby.ToString() ?? "", query.IsDecsending).AsQueryable();

                var employees = EmployeePagination.Pagination(employessAssignedInTheProject, query.PageNumber, query.PageSize).ToList();
                project.EmployeeAssignments = employees;
            }

            return project;
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.Projects.AnyAsync(e => e.ProjectUID == id);
        }

        public async Task<Project?> UpdateAsync(Guid id, Project project)
        {
            var existingProject = await _context.Projects.FirstOrDefaultAsync(e => e.ProjectUID == id);
            if (existingProject != null)
            {
                existingProject.ProjectPub_ID = project.ProjectPub_ID;
                existingProject.ProjectName = project.ProjectName;
                existingProject.Description = project.Description;
                existingProject.StartDate = project.StartDate;
                existingProject.EndDate = project.EndDate;
                existingProject.Status = project.Status;
                _context.Projects.Update(existingProject);
                await _context.SaveChangesAsync();
                return existingProject;
            }
            return null;
        }
    }
}
