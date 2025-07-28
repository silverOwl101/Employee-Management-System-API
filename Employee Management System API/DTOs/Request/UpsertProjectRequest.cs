using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertProjectRequest
    {
        [Required, MaxLength(10)]
        public string ProjectPub_ID { get; set; } = default!;

        [Required, MaxLength(100)]
        public string ProjectName { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public ProjectStatus Status { get; set; }        
    }
}
