using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Response
{
    public class PayrollResponse
    {
        public string PayrollPub_ID { get; set; } = default!;
        public DateOnly PayDate { get; set; }
       
        public decimal BasicSalary { get; set; }

        public decimal Allowances { get; set; }

        public decimal Deductions { get; set; }

        public decimal NetSalary { get; set; }
    }
}