using Employee_Management_System_API.Queries.Base;
using static Employee_Management_System_API.Domain.Enums.PerformanceReviewCategories;

namespace Employee_Management_System_API.Queries.PerformanceReview
{
    public class QueryGetAllPerformanceReview : QuerySortingAndPaginationBase
    {
        /// <summary>
        /// Review public id filter for performance review record.
        /// </summary>
        public string? ReviewPub_ID { get; set; }

        /// <summary>
        /// Review date filter for performance review record.
        /// </summary>
        public DateOnly? ReviewDate { get; set; }

        /// <summary>
        /// Score filter for performance review record.
        /// </summary>
        public int? Score { get; set; }

        /// <summary>
        /// Sort by filter for performance review record.
        /// </summary>
        public SortGetAllPerformanceReview? Sortby { get; set; }
    }
}
