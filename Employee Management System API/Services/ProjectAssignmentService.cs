using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Helpers;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Mappings;
using Employee_Management_System_API.Queries.ProjectAssignment;

namespace Employee_Management_System_API.Services
{
    public class ProjectAssignmentService : IProjectAssignmentService
    {
        private readonly IProjectAssignmentRepository _projectAssignmentRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IProjectRepository _projectRepo;
        public ProjectAssignmentService(IProjectAssignmentRepository projectAssignmentRepo,
                                        IEmployeeRepository employeeRepo,
                                        IProjectRepository projectRepo)
        {
            _projectAssignmentRepo = projectAssignmentRepo;
            _employeeRepo = employeeRepo;
            _projectRepo = projectRepo;
        }
        public async Task<ProjectAssignmentResponse> CreateProjectAssignment(UpsertProjectAssignmentRequest projectAssignment)
        {
            var existingEmployee = await _employeeRepo.GetByIdAsync(projectAssignment.EmployeePub_ID);
            var existingProject = await _projectRepo.GetByIdAsync(projectAssignment.ProjectPub_ID, null);

            if (existingProject == null)
                throw new KeyNotFoundException("Project not found!");

            if (existingEmployee == null)
                throw new KeyNotFoundException("Employee not found!");

            if (!ValidationHelper.isRegexMatch(projectAssignment.AssignmentPub_ID))
                throw new InvalidOperationException($"Project assignment ID must be in the" +
                                                    $"format 0000-0000 using only digits.");

            var initProjectAssignment = new ProjectAssignment
            {
                AssignmentPub_ID = projectAssignment.AssignmentPub_ID,
                RoleInProject = projectAssignment.RoleInProject,
                AssignedDate = projectAssignment.AssignedDate,
                EmployeeUID = existingEmployee.EmployeeUID,
                ProjectUID = existingProject.ProjectUID
            };

            var createdProject = await _projectAssignmentRepo.CreateAsync(initProjectAssignment);
            return new ProjectAssignmentResponse
            {
                AssignmentPub_ID = createdProject.AssignmentPub_ID,
                FirstName = existingEmployee.FirstName,
                MiddleName = existingEmployee.MiddleName,
                LastName = existingEmployee.LastName,
                ProjectName = existingProject.ProjectName,
                RoleInProject = createdProject.RoleInProject,
                AssignedDate = projectAssignment.AssignedDate
            };
        }

        public async Task<bool> DeleteProjectAssignment(string id)
        {
            var exist = await _projectAssignmentRepo.GetByIdAsync(id);
            var result = exist != null ? await _projectAssignmentRepo.DeleteAsync(exist.AssignmentUID) : false;
            return result;
        }

        public async Task<IEnumerable<ProjectAssignmentResponse>> GetAllProjectAssignment(QueryGetAllProjectAssignment query)
        {
            var list = await _projectAssignmentRepo.GetAllAsync(query);
            return list.Select(e => e.ToProjectAssignmentDto()).ToList();
        }

        public async Task<ProjectAssignmentResponse?> GetProjectAssignmentById(string id)
        {
            var existingProjectAssignment = await _projectAssignmentRepo.GetByIdAsync(id);
            if (existingProjectAssignment != null)
                return existingProjectAssignment.ToProjectAssignmentDto();
            throw new KeyNotFoundException("No records found.");
        }

        public async Task<bool> IsProjectAssignmentExists(Guid id)
        {
            return await _projectAssignmentRepo.IsExistsAsync(id);
        }

        public async Task<ProjectAssignmentResponse?> UpdateProjectAssignment(string id, UpsertProjectAssignmentRequest projectAssignment)
        {
            var existingProjectAssignment = await _projectAssignmentRepo.GetByIdAsync(id);
            if (existingProjectAssignment != null)
            {
                var updated = new ProjectAssignment
                {
                    AssignmentPub_ID = projectAssignment.AssignmentPub_ID,
                    RoleInProject = projectAssignment.RoleInProject,
                    AssignedDate = projectAssignment.AssignedDate
                };
                var newProjectAssignmentValue = await _projectAssignmentRepo.UpdateAsync(existingProjectAssignment.AssignmentUID, updated);
                if (newProjectAssignmentValue != null)
                    return newProjectAssignmentValue.ToProjectAssignmentDto();
                throw new InvalidOperationException("Project assignment cannot be updated!");
            }
            throw new InvalidOperationException("Project assignment cannot be updated!");
        }
    }
}
