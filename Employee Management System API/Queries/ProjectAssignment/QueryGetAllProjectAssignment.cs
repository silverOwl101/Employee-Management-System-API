using Employee_Management_System_API.Queries.Base;
using static Employee_Management_System_API.Domain.Enums.ProjectAssignmentCategories;

namespace Employee_Management_System_API.Queries.ProjectAssignment
{
    public class QueryGetAllProjectAssignment : QuerySortingAndPaginationBase
    {
        /// <summary>
        /// Project assignment public id filtering for project assignment record.
        /// </summary>
        public string? AssignmentPub_ID { get; set; }

        /// <summary>
        /// Employee role in the project filtering for project assignment record.
        /// </summary>
        public string? RoleInProject { get; set; }

        /// <summary>
        /// Assigned date filtering for project assignment record.
        /// </summary>
        public DateOnly? AssignedDate { get; set; }

        /// <summary>
        /// Sort by filtering for project assignment record.
        /// </summary>
        public GetAllProjectAssignment? Sortby { get; set; }
    }
}
