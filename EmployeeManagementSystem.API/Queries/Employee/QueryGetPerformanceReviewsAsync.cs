using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Queries.Employee
{
    public class QueryGetPerformanceReviewsAsync : QuerySortingAndPaginationBase
    {
        /// <summary>
        /// Review public id filter for the employee performance review records
        /// </summary>       
        public string? ReviewPub_ID { get; set; }

        /// <summary>
        /// Review date filter for the employee performance review records
        /// </summary>       
        public DateOnly? ReviewDate { get; set; }

        /// <summary>
        /// Score filter for the employee performance review records
        /// </summary>       
        [Range(1, 10)]
        public int? Score { get; set; }

        /// <summary>
        /// Sort by filter for the employee performance review records
        /// </summary>       
        public SortGetPerformanceReviewsAsync? Sortby { get; set; }
    }
}
