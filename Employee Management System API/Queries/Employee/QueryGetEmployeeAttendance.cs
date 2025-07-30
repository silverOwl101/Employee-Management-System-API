using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Queries.Employee
{
    public class QueryGetEmployeeAttendance : QuerySortingAndPaginationBase
    {
        /// <summary>
        /// Attendance public id filter for the employee attendance records
        /// </summary>        
        public string? AttendancePub_ID { get; set; } = default!;
        /// <summary>
        /// Date filter for the employee attendance records
        /// </summary>        
        public DateOnly? Date { get; set; }

        /// <summary>
        /// Check in time filter for the employee attendance records
        /// </summary>        
        public TimeSpan? CheckInTime { get; set; }

        /// <summary>
        /// Check out time filter for the employee attendance records
        /// </summary>        
        public TimeSpan? CheckOutTime { get; set; }

        /// <summary>
        /// Sort by filter for the employee attendance records
        /// </summary>        
        public SortGetAttendancesAsync? Sortby { get; set; }
    }
}
