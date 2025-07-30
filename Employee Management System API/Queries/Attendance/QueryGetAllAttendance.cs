using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;
using static Employee_Management_System_API.Domain.Enums.AttendanceCategories;

namespace Employee_Management_System_API.Queries.Attendance
{
    public class QueryGetAllAttendance : QuerySortingAndPaginationBase
    {
        /// <summary>
        /// Attendance public id filter for the attendance record
        /// </summary>
        public string? AttendancePub_ID { get; set; } = default!;

        /// <summary>
        /// Date filter for the attendance record
        /// </summary>
        public DateOnly? Date { get; set; }

        /// <summary>
        /// Check in time filter for the attendance record
        /// </summary>
        public TimeSpan? CheckInTime { get; set; }

        /// <summary>
        /// Check out time filter for the attendance record
        /// </summary>
        public TimeSpan? CheckOutTime { get; set; }

        /// <summary>
        /// Status filter for the attendance record
        /// </summary>
        public AttendanceStatus? Status { get; set; }

        /// <summary>
        /// Sort by filter for the attendance record
        /// </summary>
        public SortGetAllAttendanceAsync? Sortby { get; set; }
    }
}
