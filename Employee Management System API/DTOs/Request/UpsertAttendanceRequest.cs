using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertAttendanceRequest
    {
        [Required, MaxLength(10)]
        [DisplayName("Attendance ID")]
        public string AttendancePub_ID { get; set; } = default!;

        [Required]
        [DisplayName("Attendance date")]
        public DateOnly Date { get; set; }

        [Required]
        [DisplayName("Time in")]
        public TimeSpan CheckInTime { get; set; }

        [Required]
        [DisplayName("Time out")]
        public TimeSpan CheckOutTime { get; set; }

        [Required]
        [DisplayName("Status")]
        public AttendanceStatus Status { get; set; }

        [Required, MaxLength(10)]
        [DisplayName("Employee ID")]
        public string EmployeePub_ID { get; set; } = default!;
    }
}
