using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;
using static Employee_Management_System_API.Domain.Enums.LeaveRequestCategories;

namespace Employee_Management_System_API.Queries.LeaveRequest
{
    public class QueryGetAllLeaveRequest : QuerySortingAndPaginationBase
    {        
        /// <summary>
        /// Leave public id filter for leave request record.
        /// </summary>
        public string? LeavePub_ID { get; set; }

        /// <summary>
        /// Start date filter for leave request record.
        /// </summary>
        public DateOnly? StartDate { get; set; }

        /// <summary>
        /// End date filter for leave request record.
        /// </summary>
        public DateOnly? EndDate { get; set; }

        /// <summary>
        /// Leave type filter for leave request record.
        /// </summary>
        public LeaveType? LeaveType { get; set; }

        /// <summary>
        /// Status filter for leave request record.
        /// </summary>
        public LeaveStatus? Status { get; set; }

        /// <summary>
        /// Sort by filter for leave request record.
        /// </summary>
        public SortGetAllLeaveRequest? Sortby { get; set; }
    }
}
