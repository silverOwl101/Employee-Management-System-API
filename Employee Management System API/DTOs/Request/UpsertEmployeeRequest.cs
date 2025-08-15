using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertEmployeeRequest
    {
        [Required(ErrorMessage = "Employee ID is required.")]
        [MaxLength(10, ErrorMessage = "Employee ID cannot exceed 10 characters.")]
        [JsonPropertyName("Employee ID")]
        public string EmployeePub_ID { get; set; } = default!;

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = default!;

        [Required, MaxLength(50)]
        public string MiddleName { get; set; } = default!;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = default!;
        
        [Required(ErrorMessage = "Email address is required.")]
        [MaxLength(100, ErrorMessage = "Email address cannot exceed 100 characters.")]
        [JsonPropertyName("Email address")]
        public string Email { get; set; } = default!;

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public DateOnly HireDate { get; set; }

        [Required]
        public string Address { get; set; } = default!;

        [Required]
        public EmployeeStatus Status { get; set; }
        
        [Required(ErrorMessage = "Department ID is required.")]
        [MaxLength(10, ErrorMessage = "Department ID cannot exceed 10 characters.")]
        [JsonPropertyName("Department ID")]
        public string DepartmentPub_ID { get; set; } = default!;

        [Required]
        public string RolePub_ID { get; set; } = default!;

        [Required]
        public Guid AppUserId { get; set; } = default!;
    }
}
