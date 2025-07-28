using Employee_Management_System_API.Queries.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Queries.Employee
{
    public class QueryGetEmployeePayroll : QuerySortingAndPaginationBase
    {
        [Required]
        public string EmployeePub_ID { get; set; } = default!;
        public string? PayrollPub_ID { get; set; }
       
        public DateOnly? PayDate { get; set; }
        public SortGetPayrollsAsync? Sortby { get; set; }
    }
}
