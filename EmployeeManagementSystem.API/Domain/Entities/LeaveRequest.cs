using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.Domain.Entities
{
    public class LeaveRequest
    {
        [Key]
        public Guid LeaveUID { get; set; }

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

        public Guid EmployeeUID { get; set; }
        public Employee Employee { get; set; } = default!;

    }
}
