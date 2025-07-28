using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Queries.Project;

namespace Employee_Management_System_API.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync(QueryGetAllProject query);
        Task<Project?> GetByIdAsync(string id, QueryGetById? query);
        Task<Project> CreateAsync(Project project);
        Task<Project?> UpdateAsync(Guid id, Project project);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> IsExistsAsync(Guid id);
    }
}
