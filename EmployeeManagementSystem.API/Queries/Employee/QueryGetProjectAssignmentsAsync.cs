using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Queries.Employee
{
    public class QueryGetProjectAssignmentsAsync : QuerySortingAndPaginationBase
    {
        /// <summary>
        /// Assignment public id filter for the employee project assignment records
        /// </summary>       
        [MaxLength(10)]
        public string? AssignmentPub_ID { get; set; }

        /// <summary>
        /// Employee role in the project filter for the employee project assignment records
        /// </summary>       
        [MaxLength(100)]
        public string? RoleInProject { get; set; }

        /// <summary>
        /// Assignment date filter for the employee project assignment records
        /// </summary>       
        public DateOnly? AssignedDate { get; set; }

        /// <summary>
        /// Sort by filter for the employee project assignment records
        /// </summary>       
        public SortGetProjectAssignmentsAsync? Sortby { get; set; }
    }
}
