using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Queries.Employee
{
    public class QueryGetProjectAssignmentsAsync : QuerySortingAndPaginationBase
    {
        [Required]
        public string EmployeePub_ID { get; set; } = default!;

        [MaxLength(10)]
        public string? AssignmentPub_ID { get; set; }

        [MaxLength(100)]
        public string? RoleInProject { get; set; }
        
        public DateOnly? AssignedDate { get; set; }

        public SortGetProjectAssignmentsAsync? Sortby { get; set; }
    }
}
