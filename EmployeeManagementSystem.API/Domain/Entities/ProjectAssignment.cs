using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.Domain.Entities
{
    public class ProjectAssignment
    {
        [Key]
        public Guid AssignmentUID { get; set; }

        [Required, MaxLength(10)]
        public string AssignmentPub_ID { get; set; } = default!;

        [Required, MaxLength(100)]
        public string RoleInProject { get; set; } = default!;

        [Required]
        public DateOnly AssignedDate { get; set; }

        public Guid EmployeeUID { get; set; }
        public Employee Employee { get; set; } = default!;

        public Guid ProjectUID { get; set; }
        public Project Project { get; set; } = default!;

    }
}
