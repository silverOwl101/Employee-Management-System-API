using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Queries.PerformanceReview;

namespace Employee_Management_System_API.Interfaces.Repositories
{
    public interface IPerformanceReviewRepository
    {
        Task<IEnumerable<PerformanceReview>> GetAllAsync(QueryGetAllPerformanceReview query);
        Task<PerformanceReview?> GetByIdAsync(string id);
        Task<PerformanceReview> CreateAsync(PerformanceReview performanceReview);
        Task<PerformanceReview?> UpdateAsync(Guid id, PerformanceReview performanceReview);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> IsExistsAsync(Guid id);
    }
}
