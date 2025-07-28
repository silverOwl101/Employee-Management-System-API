using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertProjectAssignmentRequest
    {
        [Required, MaxLength(10)]
        public string AssignmentPub_ID { get; set; } = default!;

        [Required, MaxLength(100)]
        public string RoleInProject { get; set; } = default!;

        [Required]
        public DateOnly AssignedDate { get; set; }

        [Required, MaxLength(10)]
        public string EmployeePub_ID { get; set; } = default!;

        [Required, MaxLength(10)]
        public string ProjectPub_ID { get; set; } = default!;
    }
}
