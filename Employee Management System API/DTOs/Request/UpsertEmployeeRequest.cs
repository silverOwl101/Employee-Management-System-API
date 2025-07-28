using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertEmployeeRequest
    {
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
        public string DepartmentPub_ID { get; set; } = default!;

        [Required]
        public string RolePub_ID { get; set; } = default!;
        
        [Required]
        public Guid AppUserId { get; set; } = default!;
    }
}
