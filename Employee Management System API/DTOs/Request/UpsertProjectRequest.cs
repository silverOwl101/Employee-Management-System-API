using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertProjectRequest
    {
        [Required, MaxLength(10)]
        [DisplayName("Project ID")]
        public string ProjectPub_ID { get; set; } = default!;

        [Required, MaxLength(100)]
        [DisplayName("Project name")]
        public string ProjectName { get; set; } = default!;

        [Required]
        [DisplayName("Project description")]
        public string Description { get; set; } = default!;

        [Required]
        [DisplayName("Project start date")]
        public DateOnly StartDate { get; set; }

        [Required]
        [DisplayName("Project end date")]
        public DateOnly EndDate { get; set; }

        [Required]
        [DisplayName("Project status")]
        public ProjectStatus Status { get; set; }        
    }
}
