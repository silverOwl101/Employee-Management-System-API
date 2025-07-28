using Employee_Management_System_API.Queries.Base;
using static Employee_Management_System_API.Domain.Enums.PerformanceReviewCategories;

namespace Employee_Management_System_API.Queries.PerformanceReview
{
    public class QueryGetAllPerformanceReview : QuerySortingAndPaginationBase
    {
        public string? ReviewPub_ID { get; set; }

        public DateOnly? ReviewDate { get; set; }

        public int? Score { get; set; }
        public SortGetAllPerformanceReview? Sortby { get; set; }
    }
}
