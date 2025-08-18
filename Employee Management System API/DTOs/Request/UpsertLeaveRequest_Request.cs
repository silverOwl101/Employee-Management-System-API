using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertLeaveRequest_Request
    {
        [Required, MaxLength(10)]
        [DisplayName("Leave request ID")]
        public string LeavePub_ID { get; set; } = default!;

        [Required]
        [DisplayName("Leave request start date")]
        public DateOnly StartDate { get; set; }

        [Required]
        [DisplayName("Leave request end date")]
        public DateOnly EndDate { get; set; }

        [Required]
        [DisplayName("Leave request type")]
        public LeaveType LeaveType { get; set; }

        [Required]
        [DisplayName("Leave request status")]
        public LeaveStatus Status { get; set; }

        [Required]
        [DisplayName("Reason for leave request")]
        public string Reason { get; set; } = default!;

        [Required, MaxLength(10)]
        [DisplayName("Employee ID")]
        public string EmployeePub_ID { get; set; } = default!;
    }
}
