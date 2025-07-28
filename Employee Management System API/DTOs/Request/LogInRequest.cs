using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class LogInRequest
    {
        [Required]
        public string UserName { get; set; } = default!;
        
        [Required]
        public string PassWord { get; set; } = default!;
    }
}
