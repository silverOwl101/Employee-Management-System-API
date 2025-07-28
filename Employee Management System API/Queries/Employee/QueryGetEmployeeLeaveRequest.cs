using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Queries.Employee
{
    public class QueryGetEmployeeLeaveRequest : QuerySortingAndPaginationBase
    {
        [Required]
        public string EmployeePub_ID { get; set; } = default!;
        public string? LeavePub_ID { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public LeaveType? LeaveType { get; set; }
        public LeaveStatus? Status { get; set; }
        public SortGetLeaveRequestsAsync? Sortby { get; set; }
    }
}
