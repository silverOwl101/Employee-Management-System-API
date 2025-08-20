using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.Domain.Entities
{
    public class Role
    {
        [Key]
        public Guid RoleUID { get; set; }

        [Required, MaxLength(10)]
        public string RolePub_ID { get; set; } = default!;

        [Required, MaxLength(100)]
        public string RoleName { get; set; } = default!;

        public string? Description { get; set; }

        [Required, Precision(10, 2)]
        public decimal Salary { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
