using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Queries.Employee
{
    public class QueryGetEmployeeAttendance : QuerySortingAndPaginationBase
    {
        [Required]
        public string EmployeePub_ID { get; set; } = default!;
        public string? AttendancePub_ID { get; set; }
        public DateOnly? Date { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public SortGetAttendancesAsync? Sortby { get; set; }
    }
}
