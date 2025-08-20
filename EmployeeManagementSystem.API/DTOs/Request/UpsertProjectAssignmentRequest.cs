using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertProjectAssignmentRequest
    {
        [Required, MaxLength(10)]
        [DisplayName("Project assignment ID")]
        public string AssignmentPub_ID { get; set; } = default!;

        [Required, MaxLength(100)]
        [DisplayName("Employee role in the project")]
        public string RoleInProject { get; set; } = default!;

        [Required]
        [DisplayName("Date assigned")]
        public DateOnly AssignedDate { get; set; }

        [Required, MaxLength(10)]
        [DisplayName("Employee ID")]
        public string EmployeePub_ID { get; set; } = default!;

        [Required, MaxLength(10)]
        [DisplayName("Project ID")]
        public string ProjectPub_ID { get; set; } = default!;
    }
}
