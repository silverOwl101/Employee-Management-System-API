using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.Domain.Entities
{
    public class Employee
    {
        [Key]
        public Guid EmployeeUID { get; set; }

        [Required, MaxLength(10)]
        public string EmployeePub_ID { get; set; } = default!;

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = default!;

        [Required, MaxLength(50)]
        public string MiddleName { get; set; } = default!;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = default!;

        [Required, MaxLength(100)]
        public string Email { get; set; } = default!;

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public DateOnly HireDate { get; set; }

        [Required]
        public string Address { get; set; } = default!;

        [Required]
        public EmployeeStatus Status { get; set; }

        [Required]
        public Guid DepartmentUID { get; set; } = default!;
        public Department Department { get; set; } = default!;

        [Required]
        public Guid RoleUID { get; set; } = default!;
        public Role Role { get; set; } = default!;
        
        public Guid? AppUserId { get; set; } = default!;
        public AppUser AppUser { get; set; } = default!;

        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = [];
        public ICollection<Attendance> Attendances { get; set; } = [];
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = [];
        public ICollection<Payroll> Payrolls { get; set; } = [];
        public ICollection<ProjectAssignment> ProjectAssignments { get; set; } = [];
        public ICollection<PerformanceReview> PerformanceReviews { get; set; } = [];

    }
}
