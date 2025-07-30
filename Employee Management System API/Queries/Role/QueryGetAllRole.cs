using Employee_Management_System_API.Queries.Base;
using static Employee_Management_System_API.Domain.Enums.RoleCategories;

namespace Employee_Management_System_API.Queries.Role
{
    public class QueryGetAllRole : QuerySortingAndPaginationBase
    {
        /// <summary>
        /// Role public id filter for role records.
        /// </summary>
        public string? RolePub_ID { get; set; }

        /// <summary>
        /// Role name filter for role records.
        /// </summary>
        public string? RoleName { get; set; }

        /// <summary>
        /// Sort by filter for role records.
        /// </summary>
        public SortGetAllRoles? Sortby { get; set; }
    }
}
