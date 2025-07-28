using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Response
{
    public class PerformanceReviewResponse
    {
        public string ReviewPub_ID { get; set; } = default!;
        
        public DateOnly ReviewDate { get; set; }
      
        public int Score { get; set; }

        public string? Comments { get; set; }
    }
}