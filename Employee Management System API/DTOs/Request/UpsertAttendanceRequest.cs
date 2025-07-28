using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertAttendanceRequest
    {
        [Required, MaxLength(10)]
        public string AttendancePub_ID { get; set; } = default!;

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeSpan CheckInTime { get; set; }

        [Required]
        public TimeSpan CheckOutTime { get; set; }

        [Required]
        public AttendanceStatus Status { get; set; }

        [Required, MaxLength(10)]
        public string EmployeePub_ID { get; set; } = default!;
    }
}
