using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;
using static Employee_Management_System_API.Domain.Enums.ProjectCategories;

namespace Employee_Management_System_API.Queries.Project
{
    public class QueryGetAllProject : QuerySortingAndPaginationBase
    {
        public string? ProjectPub_ID { get; set; }

        public string? ProjectName { get; set; }
        
        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public ProjectStatus? Status { get; set; }

        public SortByGetAllProject? Sortby { get; set; }
    }
}
