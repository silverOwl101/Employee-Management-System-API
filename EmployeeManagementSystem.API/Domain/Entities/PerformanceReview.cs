using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.Domain.Entities
{
    public class PerformanceReview
    {
        [Key]
        public Guid ReviewUID { get; set; }

        [Required, MaxLength(10)]
        public string ReviewPub_ID { get; set; } = default!;

        [Required]
        public DateOnly ReviewDate { get; set; }

        [Required, Range(1, 10)]
        public int Score { get; set; }

        public string? Comments { get; set; }

        public Guid EmployeeUID { get; set; }
        public Employee Employee { get; set; } = default!;

    }
}
