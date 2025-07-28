using Employee_Management_System_API.Queries.Base;
using static Employee_Management_System_API.Domain.Enums.ProjectAssignmentCategories;

namespace Employee_Management_System_API.Queries.ProjectAssignment
{
    public class QueryGetAllProjectAssignment : QuerySortingAndPaginationBase
    {
        public string? AssignmentPub_ID { get; set; }

        public string? RoleInProject { get; set; }

        public DateOnly? AssignedDate { get; set; }

        public GetAllProjectAssignment? Sortby { get; set; }
    }
}
