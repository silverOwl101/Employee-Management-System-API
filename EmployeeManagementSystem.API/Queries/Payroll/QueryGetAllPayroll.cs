using Employee_Management_System_API.Queries.Base;
using static Employee_Management_System_API.Domain.Enums.PayrollCategories;

namespace Employee_Management_System_API.Queries.Payroll
{
    public class QueryGetAllPayroll : QuerySortingAndPaginationBase
    {
        /// <summary>
        /// Payroll public id filter for payroll records.
        /// </summary>
        public string? PayrollPub_ID { get; set; }

        /// <summary>
        /// Pay date filter for payroll records.
        /// </summary>
        public DateOnly? PayDate { get; set; }

        /// <summary>
        /// Sort by filter for payroll records.
        /// </summary>
        public SortGetAllPayroll? Sortby { get; set; }
    }
}
