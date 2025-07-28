using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;
using static Employee_Management_System_API.Domain.Enums.LeaveRequestCategories;

namespace Employee_Management_System_API.Queries.LeaveRequest
{
    public class QueryGetAllLeaveRequest : QuerySortingAndPaginationBase
    {        
        public string? LeavePub_ID { get; set; }
     
        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public LeaveType? LeaveType { get; set; }
        
        public LeaveStatus? Status { get; set; }
        public SortGetAllLeaveRequest? Sortby { get; set; }
    }
}
