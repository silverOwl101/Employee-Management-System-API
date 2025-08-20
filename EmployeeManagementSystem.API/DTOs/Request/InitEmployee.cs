using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Request
{
    public class InitEmployee
    {
        [Required, MaxLength(10)]
        [DisplayName("Employee ID")]
        public string EmployeePub_ID { get; set; } = default!;

        [Required, MaxLength(50)]
        [DisplayName("First name")]
        public string FirstName { get; set; } = default!;

        [Required, MaxLength(50)]
        [DisplayName("Middle name")]
        public string MiddleName { get; set; } = default!;

        [Required, MaxLength(50)]
        [DisplayName("Last name")]
        public string LastName { get; set; } = default!;

        [Required, MaxLength(100)]
        [DisplayName("Email address")]
        public string Email { get; set; } = default!;

        [Required]
        [DisplayName("Date of birth")]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [DisplayName("Hire date")]
        public DateOnly HireDate { get; set; }

        [Required]
        [DisplayName("Address")]
        public string Address { get; set; } = default!;

        [Required]
        [DisplayName("Employment status")]
        public EmployeeStatus Status { get; set; }

        [Required]
        [DisplayName("Department ID")]
        public string DepartmentPub_ID { get; set; } = default!;

        [Required]
        [DisplayName("Role ID")]
        public string RolePub_ID { get; set; } = default!;
    }
}
