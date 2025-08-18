using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Request
{
    public class InsertEmployeeRequest
    {
        [Required, MaxLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = default!;

        [Required, MaxLength(50)]
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; } = default!;

        [Required, MaxLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; } = default!;

        [Required, MaxLength(100)]
        [DisplayName("Email Address")]
        public string Email { get; set; } = default!;

        [Required]
        [DisplayName("Date of birth")]
        public DateOnly DateOfBirth { get; set; } = default!;

        [Required]
        [DisplayName("Hired Date")]
        public DateOnly HireDate { get; set; } = default!;

        [Required]
        [DisplayName("Address")]
        public string Address { get; set; } = default!;

        [Required]
        [DisplayName("Employment status")]
        public EmployeeStatus Status { get; set; } = default!;

        [Required]
        [DisplayName("Department ID")]
        public string DepartmentPub_ID { get; set; } = default!;

        [Required]
        [DisplayName("Role ID")]
        public string RolePub_ID { get; set; } = default!;
    }
}
