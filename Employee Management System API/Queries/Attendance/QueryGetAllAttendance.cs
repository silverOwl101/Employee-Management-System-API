using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;
using static Employee_Management_System_API.Domain.Enums.AttendanceCategories;

namespace Employee_Management_System_API.Queries.Attendance
{
    public class QueryGetAllAttendance : QuerySortingAndPaginationBase
    {
        public string? AttendancePub_ID { get; set; } = default!;
        
        public DateOnly? Date { get; set; }

        public TimeSpan? CheckInTime { get; set; }

        public TimeSpan? CheckOutTime { get; set; }

        public AttendanceStatus? Status { get; set; }

        public SortGetAllAttendanceAsync? Sortby { get; set; }
    }
}
