using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.Domain.Entities
{
    public class Payroll
    {
        [Key]
        public Guid PayrollUID { get; set; }

        [Required, MaxLength(10)]
        public string PayrollPub_ID { get; set; } = default!;

        [Required]
        public DateOnly PayDate { get; set; }

        [Required, Precision(10, 2)]
        public decimal BasicSalary { get; set; }

        [Required, Precision(10, 2)]
        public decimal Allowances { get; set; }

        [Required, Precision(10, 2)]
        public decimal Deductions { get; set; }

        [Required, Precision(10, 2)]
        public decimal NetSalary { get; set; }

        public Guid EmployeeUID { get; set; }
        public Employee Employee { get; set; } = default!;

    }
}
