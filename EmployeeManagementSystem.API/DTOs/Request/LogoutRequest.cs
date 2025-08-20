using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class LogoutRequest
    {
        [DisplayName("_rt")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
