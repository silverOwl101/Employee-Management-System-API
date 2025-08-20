using Employee_Management_System_API.Queries.Base;
using static Employee_Management_System_API.Domain.Enums.Categories;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Queries.Employee
{
    public class QueryGetEmployeeLeaveRequest : QuerySortingAndPaginationBase
    {
        /// <summary>
        /// Leave public id filter for the employee leave request records
        /// </summary>        
        public string? LeavePub_ID { get; set; }

        /// <summary>
        /// Start date filter for the employee leave request records
        /// </summary>        
        public DateOnly? StartDate { get; set; }

        /// <summary>
        /// End date filter for the employee leave request records
        /// </summary>        
        public DateOnly? EndDate { get; set; }

        /// <summary>
        /// Leave type filter for the employee leave request records
        /// </summary>       
        public LeaveType? LeaveType { get; set; }

        /// <summary>
        /// Leave status filter for the employee leave request records
        /// </summary>       
        public LeaveStatus? Status { get; set; }

        /// <summary>
        /// Sort by filter for the employee leave request records
        /// </summary>       
        public SortGetLeaveRequestsAsync? Sortby { get; set; }
    }
}
