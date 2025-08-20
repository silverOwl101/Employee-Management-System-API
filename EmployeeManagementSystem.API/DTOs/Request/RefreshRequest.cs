using System.ComponentModel;

namespace Employee_Management_System_API.DTOs.Request
{
    public class RefreshRequest
    {
        [DisplayName("_rt")]
        public string RefreshToken { get; set; } = string.Empty;
        [DisplayName("_ed")]
        public string EmployeeId { get; set; } = string.Empty;
    }
}
