using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertLeaveRequest_Request
    {
        [Required, MaxLength(10)]
        public string LeavePub_ID { get; set; } = default!;

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public LeaveType LeaveType { get; set; }

        [Required]
        public LeaveStatus Status { get; set; }

        [Required]
        public string Reason { get; set; } = default!;

        [Required, MaxLength(10)]
        public string EmployeePub_ID { get; set; } = default!;
    }
}
