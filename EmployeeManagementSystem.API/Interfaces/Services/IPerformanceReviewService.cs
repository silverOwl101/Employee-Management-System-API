using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Queries.PerformanceReview;

namespace Employee_Management_System_API.Interfaces.Services
{
    public interface IPerformanceReviewService
    {
        Task<IEnumerable<PerformanceReviewResponse>> GetAllPerformanceReviewAsync(QueryGetAllPerformanceReview query);
        Task<PerformanceReviewResponse?> GetPerformanceReviewByIdAsync(string id);
        Task<PerformanceReviewResponse> CreatePerformanceReviewAsync(UpsertPerformanceReviewRequest performanceReview);
        Task<PerformanceReviewResponse?> UpdatePerformanceReviewAsync(string id, UpsertPerformanceReviewRequest performanceReview);
        Task<bool> DeletePerformanceReviewAsync(string id);
        Task<bool> IsPerformanceReviewExistsAsync(string id);
    }
}
