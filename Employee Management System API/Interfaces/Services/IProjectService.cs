using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Queries.Project;

namespace Employee_Management_System_API.Interfaces.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponse>> GetAllProjectAsync(QueryGetAllProject query);
        Task<IEnumerable<EmployeeAssignedResponse>> GetAssignedEmployeesOfTheProject(string id, QueryGetById query);
        Task<ProjectResponse?> GetProjectByIdAsync(string id);
        Task<ProjectResponse> CreateProjectAsync(UpsertProjectRequest project);
        Task<ProjectResponse?> UpdateProjectAsync(string id, UpsertProjectRequest project);
        Task<bool> DeleteProjectAsync(string id);
        Task<bool> IsProjectExistsAsync(Guid id);
    }
}
