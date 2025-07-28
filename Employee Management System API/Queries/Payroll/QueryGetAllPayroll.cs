using Employee_Management_System_API.Queries.Base;
using static Employee_Management_System_API.Domain.Enums.PayrollCategories;

namespace Employee_Management_System_API.Queries.Payroll
{
    public class QueryGetAllPayroll : QuerySortingAndPaginationBase
    {
        public string? PayrollPub_ID { get; set; }

        public DateOnly? PayDate { get; set; }

        public SortGetAllPayroll? Sortby { get; set; }
    }
}
