using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Response;

namespace Employee_Management_System_API.Mappings
{
    public static class PerformanceReviewMappers
    {
        public static PerformanceReviewResponse ToPerformanceReviewDto(this PerformanceReview performanceReview)
        {
            return new PerformanceReviewResponse
            {
                ReviewPub_ID = performanceReview.ReviewPub_ID,
                ReviewDate = performanceReview.ReviewDate,
                Score = performanceReview.Score,
                Comments = performanceReview.Comments
            };
        }
    }
}
