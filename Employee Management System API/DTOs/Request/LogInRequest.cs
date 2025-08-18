using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class LogInRequest
    {
        [Required]
        [DisplayName("Account name")]
        public string UserName { get; set; } = default!;
        
        [Required]
        [DisplayName("Account password")]
        public string PassWord { get; set; } = default!;
    }
}
