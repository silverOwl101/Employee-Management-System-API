using Employee_Management_System_API.Queries.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Queries.Employee
{
    public class QueryGetEmployeePayroll : QuerySortingAndPaginationBase
    {
        /// <summary>
        /// Payroll public id filter for the employee payroll records
        /// </summary>       
        public string? PayrollPub_ID { get; set; }

        /// <summary>
        /// Pay date filter for the employee payroll records
        /// </summary>       
        public DateOnly? PayDate { get; set; }

        /// <summary>
        /// Sort by filter for the employee payroll records
        /// </summary>       
        public SortGetPayrollsAsync? Sortby { get; set; }
    }
}
