using Employee_Management_System_API.Queries.Base;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.DepartmentCategories;

namespace Employee_Management_System_API.Queries.Department
{
    public class QueryGetAllDepartment : QuerySortingAndPaginationBase       
    {
        [MaxLength(10)]
        public string? DepartmentPub_ID { get; set; }

        [MaxLength(100)]
        public string? DepartmentName { get; set; }

        public QueryGetAllDepartmentSortby? Sortby { get; set; }
    }
}
