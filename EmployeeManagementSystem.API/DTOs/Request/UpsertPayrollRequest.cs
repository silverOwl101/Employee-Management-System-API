using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertPayrollRequest
    {
        [Required, MaxLength(10)]
        [DisplayName("Payroll ID")]
        public string PayrollPub_ID { get; set; } = default!;

        [Required]
        [DisplayName("Pay date")]
        public DateOnly PayDate { get; set; }

        [Required, Precision(10, 2)]
        [DisplayName("Basic salary")]
        public decimal BasicSalary { get; set; }

        [Required, Precision(10, 2)]
        [DisplayName("Allowances")]
        public decimal Allowances { get; set; }

        [Required, Precision(10, 2)]
        [DisplayName("Deductions")]
        public decimal Deductions { get; set; }

        [Required, Precision(10, 2)]
        [DisplayName("Net salary")]
        public decimal NetSalary { get; set; }

        [Required, MaxLength(10)]
        [DisplayName("Employee ID")]
        public string EmployeePub_ID { get; set; } = default!;
    }
}
