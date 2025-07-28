using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertPerformanceReviewRequest
    {
        [Required, MaxLength(10)]
        public string ReviewPub_ID { get; set; } = default!;

        [Required]
        public DateOnly ReviewDate { get; set; }

        [Required, Range(1, 10)]
        public int Score { get; set; }

        public string? Comments { get; set; }

        [Required, MaxLength(10)]
        public string EmployeePub_ID { get; set; } = default!;
    }
}
