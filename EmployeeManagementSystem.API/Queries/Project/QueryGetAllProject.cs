using Employee_Management_System_API.Queries.Base;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;
using static Employee_Management_System_API.Domain.Enums.ProjectCategories;

namespace Employee_Management_System_API.Queries.Project
{
    public class QueryGetAllProject : QuerySortingAndPaginationBase
    {
        /// <summary>
        /// Project public id filter for project record.
        /// </summary>
        public string? ProjectPub_ID { get; set; }

        /// <summary>
        /// Project name filter for project record.
        /// </summary>
        public string? ProjectName { get; set; }

        /// <summary>
        /// Start date filter for project record.
        /// </summary>
        public DateOnly? StartDate { get; set; }

        /// <summary>
        /// End date filter for project record.
        /// </summary>
        public DateOnly? EndDate { get; set; }

        /// <summary>
        /// Project status filter for project record.
        /// </summary>
        public ProjectStatus? Status { get; set; }

        /// <summary>
        /// Sort by filter for project record.
        /// </summary>
        public SortByGetAllProject? Sortby { get; set; }
    }
}
