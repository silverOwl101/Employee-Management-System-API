using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Queries.Employee
{
    public class QueryGetPhoneNumbersAsync : QuerySortingAndPaginationBase
    {
        /// <summary>
        /// Phone number public id filter for the employee phone number records
        /// </summary>       
        [MaxLength(10)]
        public string? PhoneNumberPub_ID { get; set; }

        /// <summary>
        /// Specific phone number filter for the employee phone number records
        /// </summary>       
        [MaxLength(20)]
        public string? PhoneNumberValue { get; set; }

        /// <summary>
        /// Sort by filter for the employee phone number records
        /// </summary>       
        public SortGetPhoneNumbersAsync? Sortby { get; set; }
    }
}
