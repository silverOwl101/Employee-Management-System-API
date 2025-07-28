using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Queries.Employee
{
    public class QueryGetPhoneNumbersAsync : QuerySortingAndPaginationBase
    {
        [Required]
        public string EmployeePub_ID { get; set; } = default!;

        [MaxLength(10)]
        public string? PhoneNumberPub_ID { get; set; }

        [MaxLength(20)]
        public string? PhoneNumberValue { get; set; }

        public SortGetPhoneNumbersAsync? Sortby { get; set; }
    }
}
