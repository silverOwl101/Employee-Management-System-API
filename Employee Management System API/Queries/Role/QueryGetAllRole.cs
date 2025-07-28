using Employee_Management_System_API.Queries.Base;
using static Employee_Management_System_API.Domain.Enums.RoleCategories;

namespace Employee_Management_System_API.Queries.Role
{
    public class QueryGetAllRole : QuerySortingAndPaginationBase
    {
        public string? RolePub_ID { get; set; }

        public string? RoleName { get; set; }

        public SortGetAllRoles? Sortby { get; set; }
    }
}
