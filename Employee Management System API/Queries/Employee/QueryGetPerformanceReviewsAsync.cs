using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Queries.Employee
{
    public class QueryGetPerformanceReviewsAsync : QuerySortingAndPaginationBase
    {
        [Required]
        public string EmployeePub_ID { get; set; } = default!;

        public string? ReviewPub_ID { get; set; }

        public DateOnly? ReviewDate { get; set; }

        [Range(1, 10)]
        public int? Score { get; set; }

        public SortGetPerformanceReviewsAsync? Sortby { get; set; }
    }
}
