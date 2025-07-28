using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Queries.Employee
{
    public class QueryGetAllEmployees
    {               
        public string? FirstName { get; set; } = default!;

        public string? MiddleName { get; set; } = default!;

        public string? LastName { get; set; } = default!;

        [EmailAddress]
        public string? Email { get; set; } = default!;

        public DateOnly? DateOfBirth { get; set; }

        public DateOnly? HireDate { get; set; }

        public string? Address { get; set; }

        public EmployeeStatus? Status { get; set; }

        public SortGetAllAsync? Sortby { get; set; }
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
