using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.Domain.Entities
{
    public class Department
    {
        [Key]
        public Guid DepartmentUID { get; set; }

        [Required, MaxLength(10)]
        public string DepartmentPub_ID { get; set; } = default!;

        [Required, MaxLength(100)]
        public string DepartmentName { get; set; } = default!;

        public string? Description { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
