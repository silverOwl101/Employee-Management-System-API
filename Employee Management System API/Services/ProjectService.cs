using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Helpers;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Mappings;
using Employee_Management_System_API.Queries.Project;

namespace Employee_Management_System_API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public ProjectService(IProjectRepository projectRepository, IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        public async Task<ProjectResponse> CreateProjectAsync(UpsertProjectRequest project)
        {
            if (!ValidationHelper.isRegexMatch(project.ProjectPub_ID))
                throw new InvalidOperationException($"Project ID must be in the format 0000-0000 using only digits.");

            var initProject = new Project
            {
                ProjectPub_ID = project.ProjectPub_ID,
                ProjectName = project.ProjectName,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status
            };

            var createdProject = await _projectRepository.CreateAsync(initProject);
            return createdProject.ToProjectDto();
        }

        public async Task<bool> DeleteProjectAsync(string id)
        {
            var existing = await _projectRepository.GetByIdAsync(id, null);
            var result = existing != null ? await _projectRepository.DeleteAsync(existing.ProjectUID) : false;
            return result;
        }

        public async Task<IEnumerable<ProjectResponse>> GetAllProjectAsync(QueryGetAllProject query)
        {
            var list = await _projectRepository.GetAllAsync(query);
            return list.Select(e => e.ToProjectDto()).ToList();
        }

        public async Task<IEnumerable<EmployeeAssignedResponse>> GetAssignedEmployeesOfTheProject(string projectId, QueryGetById query)
        {
            Dictionary<Guid, string> departmentLookup = new Dictionary<Guid, string>();
            var existingProject = await _projectRepository.GetByIdAsync(projectId, query);
            if (existingProject == null)
                throw new KeyNotFoundException("No project found!");
            var assignmentDetails = existingProject.EmployeeAssignments;
            var departmentGuid = existingProject.EmployeeAssignments.Select(e => e.Employee.DepartmentUID).ToList();
            var departments = await _departmentRepository.GetAllByGuidAsync(departmentGuid);
            if (departments is not null)
                departmentLookup = departments.ToDictionary(d => d!.DepartmentUID, d => d!.DepartmentName);

            var result = assignmentDetails.Select(e => new EmployeeAssignedResponse
            {
                EmployeePub_ID = e.Employee.EmployeePub_ID,
                FirstName = e.Employee.FirstName,
                MiddleName = e.Employee.MiddleName,
                LastName = e.Employee.LastName,
                RoleName = e.Employee.Role.RoleName,
                RoleInProject = e.RoleInProject,
                DepartmentName = departmentLookup.GetValueOrDefault(e.Employee.DepartmentUID, "Unknown"),
                Status = e.Employee.Status,
            });

            return result;
        }

        public async Task<ProjectResponse?> GetProjectByIdAsync(string id)
        {
            var exist = await _projectRepository.GetByIdAsync(id, null);
            if (exist != null)
                return exist.ToProjectDto();
            throw new KeyNotFoundException("No records found.");
        }

        public async Task<bool> IsProjectExistsAsync(Guid id)
        {
            return await _projectRepository.IsExistsAsync(id);
        }

        public async Task<ProjectResponse?> UpdateProjectAsync(string id, UpsertProjectRequest project)
        {
            var existing = await _projectRepository.GetByIdAsync(id, null);
            if (existing != null)
            {
                var updatedProject = new Project
                {
                    ProjectPub_ID = project.ProjectPub_ID,
                    ProjectName = project.ProjectName,
                    Description = project.Description,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Status = project.Status
                };

                var newProjectInformation = await _projectRepository.UpdateAsync(existing.ProjectUID, updatedProject);
                if (newProjectInformation != null)
                    return newProjectInformation.ToProjectDto();
                throw new InvalidOperationException("Project cannot be updated!");
            }
            throw new InvalidOperationException("Project cannot be updated!");
        }
    }
}
