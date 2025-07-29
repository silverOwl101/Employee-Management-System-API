using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Queries.Employee
{
    public class QueryGetAllEmployees
    {
        /// <summary>
        /// First name filter for the employee records
        /// </summary>
        public string? FirstName { get; set; } = default!;

        /// <summary>
        /// Middle name filter for the employee records
        /// </summary>
        public string? MiddleName { get; set; } = default!;

        /// <summary>
        /// Last name filter for the employee records
        /// </summary>
        public string? LastName { get; set; } = default!;

        /// <summary>
        /// Email filter for the employee records
        /// </summary>
        [EmailAddress]
        public string? Email { get; set; } = default!;

        /// <summary>
        /// Date of birth filter for the employee records
        /// </summary>
        public DateOnly? DateOfBirth { get; set; }

        /// <summary>
        /// Hire date filter for the employee records
        /// </summary>
        public DateOnly? HireDate { get; set; }

        /// <summary>
        /// Address filter for the employee records
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Status filter for the employee records
        /// </summary>
        public EmployeeStatus? Status { get; set; }

        /// <summary>
        /// Sortby filter for the employee records
        /// </summary>
        public SortGetAllAsync? Sortby { get; set; }

        /// <summary>
        /// Enable order by decsending of employee records
        /// </summary>
        public bool IsDecsending { get; set; } = false;

        /// <summary>
        /// Sets what page number to display in a paginated list.
        /// </summary>
        public int PageNumber { get; set; } = 1;
        
        /// <summary>
        /// Sets the number of items to display per page in a paginated list.
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
