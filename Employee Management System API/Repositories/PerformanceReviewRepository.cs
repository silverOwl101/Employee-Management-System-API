using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Helpers.DataManipulators;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Queries.PerformanceReview;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_API.Repositories
{
    public class PerformanceReviewRepository : IPerformanceReviewRepository
    {
        private readonly ApplicationDBContext _context;
        public PerformanceReviewRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<PerformanceReview> CreateAsync(PerformanceReview performanceReview)
        {
            await _context.PerformanceReviews.AddAsync(performanceReview);
            await _context.SaveChangesAsync();
            return await Task.FromResult(performanceReview);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var performanceReviewExist = await _context.PerformanceReviews.FirstOrDefaultAsync(e => e.ReviewUID == id);
            if (performanceReviewExist != null)
                _context.PerformanceReviews.Remove(performanceReviewExist);
            return await _context.SaveChangesAsync() > -1 ? true : false;
        }

        public async Task<IEnumerable<PerformanceReview>> GetAllAsync(QueryGetAllPerformanceReview query)
        {
            var performanceReview = _context.PerformanceReviews.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.ReviewPub_ID))
                performanceReview = performanceReview.Where(q => q.ReviewPub_ID == query.ReviewPub_ID);

            if (query.ReviewDate.HasValue)
                performanceReview = performanceReview.Where(q => q.ReviewDate == query.ReviewDate);

            if (query.Score.HasValue)
                performanceReview = performanceReview.Where(q => q.Score == query.Score);

            if (query.Sortby.HasValue)
                performanceReview = EmployeeSorters.Sort(performanceReview, query.Sortby.ToString() ?? "", query.IsDecsending).AsQueryable();

            return await EmployeePagination.Pagination(performanceReview, query.PageNumber, query.PageSize).ToListAsync();
        }

        public async Task<PerformanceReview?> GetByIdAsync(string id)
        {
            return await _context.PerformanceReviews.FirstOrDefaultAsync(e => e.ReviewPub_ID == id);
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.PerformanceReviews.AnyAsync(e => e.ReviewUID == id);
        }

        public async Task<PerformanceReview?> UpdateAsync(Guid id, PerformanceReview performanceReview)
        {
            var existingPerformaceReview = await _context.PerformanceReviews.FirstOrDefaultAsync(e => e.ReviewUID == id);
            if (existingPerformaceReview != null)
            {
                existingPerformaceReview.ReviewPub_ID = performanceReview.ReviewPub_ID;
                existingPerformaceReview.ReviewDate = performanceReview.ReviewDate;
                existingPerformaceReview.Score = performanceReview.Score;
                existingPerformaceReview.Comments = performanceReview.Comments;
                _context.PerformanceReviews.Update(existingPerformaceReview);
                await _context.SaveChangesAsync();
                return existingPerformaceReview;
            }
            return null;
        }
    }
}
