using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertPayrollRequest
    {
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

        [Required, MaxLength(10)]
        public string EmployeePub_ID { get; set; } = default!;
    }
}
