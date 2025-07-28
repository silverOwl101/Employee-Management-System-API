using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Helpers;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Mappings;
using Employee_Management_System_API.Queries.PerformanceReview;

namespace Employee_Management_System_API.Services
{
    public class PerformanceReviewService : IPerformanceReviewService
    {
        private readonly IPerformanceReviewRepository _performanceReviewRepo;
        private readonly IEmployeeRepository _employeeRepo;
        public PerformanceReviewService(IPerformanceReviewRepository performanceReviewRepo, IEmployeeRepository employeeRepo)
        {
            _performanceReviewRepo = performanceReviewRepo;
            _employeeRepo = employeeRepo;
        }

        public async Task<PerformanceReviewResponse> CreatePerformanceReviewAsync
                                     (UpsertPerformanceReviewRequest performanceReview)
        {
            var existingEmployee = await _employeeRepo.GetByIdAsync(performanceReview.EmployeePub_ID);
            if (existingEmployee != null)
            {
                if (!ValidationHelper.isRegexMatch(performanceReview.ReviewPub_ID))
                    throw new InvalidOperationException($"Performance review ID must be in the" +
                                                        $"format 0000-0000 using only digits.");

                var newPerformanceReview = new PerformanceReview
                {
                    ReviewPub_ID = performanceReview.ReviewPub_ID,
                    ReviewDate = performanceReview.ReviewDate,
                    Score = performanceReview.Score,
                    Comments = performanceReview.Comments,
                    EmployeeUID = existingEmployee.EmployeeUID
                };

                var result = await _performanceReviewRepo.CreateAsync(newPerformanceReview);
                return result.ToPerformanceReviewDto();
            }
            throw new KeyNotFoundException("Employee not found!");
        }

        public async Task<bool> DeletePerformanceReviewAsync(string id)
        {
            var exist = await _performanceReviewRepo.GetByIdAsync(id);
            var result = exist != null ? await _performanceReviewRepo.DeleteAsync(exist.ReviewUID) : false;
            return result;
        }

        public async Task<IEnumerable<PerformanceReviewResponse>> GetAllPerformanceReviewAsync(QueryGetAllPerformanceReview query)
        {
            var list = await _performanceReviewRepo.GetAllAsync(query);
            return list.Select(e => e.ToPerformanceReviewDto()).ToList();
        }

        public async Task<PerformanceReviewResponse?> GetPerformanceReviewByIdAsync(string id)
        {
            var existingPerformanceReview = await _performanceReviewRepo.GetByIdAsync(id);
            if (existingPerformanceReview != null)
                return existingPerformanceReview.ToPerformanceReviewDto();
            throw new KeyNotFoundException("No records found.");
        }

        public async Task<bool> IsPerformanceReviewExistsAsync(string id)
        {
            var exist = await _performanceReviewRepo.GetByIdAsync(id);
            if (exist is not null)
                return await _performanceReviewRepo.IsExistsAsync(exist.ReviewUID);
            return false;
        }

        public async Task<PerformanceReviewResponse?> UpdatePerformanceReviewAsync(string id, UpsertPerformanceReviewRequest performanceReview)
        {
            var existingPerformanceReview = await _performanceReviewRepo.GetByIdAsync(id);
            if (existingPerformanceReview != null)
            {
                var updated = new PerformanceReview
                {
                    ReviewPub_ID = performanceReview.ReviewPub_ID,
                    ReviewDate = performanceReview.ReviewDate,
                    Score = performanceReview.Score,
                    Comments = performanceReview.Comments,
                };
                var newPerformaceReview = await _performanceReviewRepo.UpdateAsync(existingPerformanceReview.ReviewUID, updated);
                if (newPerformaceReview != null)
                    return newPerformaceReview.ToPerformanceReviewDto();
                throw new InvalidOperationException("Performance review cannot be updated!");
            }
            throw new InvalidOperationException("Performance review cannot be updated!");
        }
    }
}
