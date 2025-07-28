using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Response
{
    public class ProjectResponse
    {        
        public string ProjectPub_ID { get; set; } = default!;
     
        public string ProjectName { get; set; } = default!;

        public string Description { get; set; } = default!;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public ProjectStatus Status { get; set; }
    }
}
