using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Queries.ProjectAssignment;

namespace Employee_Management_System_API.Interfaces.Repositories
{
    public interface IProjectAssignmentRepository
    {
        Task<IEnumerable<ProjectAssignment>> GetAllAsync(QueryGetAllProjectAssignment query);
        Task<IEnumerable<ProjectAssignment>> GetAssignedEmployeesAsync();
        Task<ProjectAssignment?> GetByIdAsync(string id);
        Task<ProjectAssignment> CreateAsync(ProjectAssignment projectAssignment);
        Task<ProjectAssignment?> UpdateAsync(Guid id, ProjectAssignment projectAssignment);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> IsExistsAsync(Guid id);
    }
}
