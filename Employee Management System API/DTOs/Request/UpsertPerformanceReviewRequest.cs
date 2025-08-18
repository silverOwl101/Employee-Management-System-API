using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertPerformanceReviewRequest
    {
        [Required, MaxLength(10)]
        [DisplayName("Performance review ID")]
        public string ReviewPub_ID { get; set; } = default!;

        [Required]
        [DisplayName("Performance review date")]
        public DateOnly ReviewDate { get; set; }

        [Required, Range(1, 10)]
        [DisplayName("Score")]
        public int Score { get; set; }

        [DisplayName("Comments")]
        public string? Comments { get; set; }

        [Required, MaxLength(10)]
        [DisplayName("Employee ID")]
        public string EmployeePub_ID { get; set; } = default!;
    }
}
