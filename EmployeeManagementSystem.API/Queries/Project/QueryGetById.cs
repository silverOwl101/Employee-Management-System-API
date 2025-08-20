using Employee_Management_System_API.Queries.Base;
using static Employee_Management_System_API.Domain.Enums.Categories;
using static Employee_Management_System_API.Domain.Enums.ProjectCategories;

namespace Employee_Management_System_API.Queries.Project
{
    public class QueryGetById : QuerySortingAndPaginationBase
    {
        public string? EmployeePub_ID { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? RoleName { get; set; }

        public string? RoleInProject { get; set; }

        public string? DepartmentName { get; set; }

        public EmployeeStatus? Status { get; set; }

        public SortByGetById? Sortby { get; set; }
    }
}
