using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertDepartmentRequest
    {
        [Required, MaxLength(10)]
        public string DepartmentPub_ID { get; set; } = default!;

        [Required, MaxLength(100)]
        public string DepartmentName { get; set; } = default!;
        public string? Description { get; set; }
    }
}
