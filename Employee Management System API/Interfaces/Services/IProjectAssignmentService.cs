using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Queries.ProjectAssignment;

namespace Employee_Management_System_API.Interfaces.Services
{
    public interface IProjectAssignmentService
    {
        Task<IEnumerable<ProjectAssignmentResponse>> GetAllProjectAssignment(QueryGetAllProjectAssignment query);
        Task<ProjectAssignmentResponse?> GetProjectAssignmentById(string id);
        Task<ProjectAssignmentResponse> CreateProjectAssignment(UpsertProjectAssignmentRequest projectAssignment);
        Task<ProjectAssignmentResponse?> UpdateProjectAssignment(string id, UpsertProjectAssignmentRequest projectAssignment);
        Task<bool> DeleteProjectAssignment(string id);
        Task<bool> IsProjectAssignmentExists(Guid id);
    }
}
