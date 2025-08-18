using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertDepartmentRequest
    {
        [Required, MaxLength(10)]
        [DisplayName("Department ID")]
        public string DepartmentPub_ID { get; set; } = default!;

        [Required, MaxLength(100)]
        [DisplayName("Department name")]
        public string DepartmentName { get; set; } = default!;

        [DisplayName("Department description")]
        public string? Description { get; set; }
    }
}
