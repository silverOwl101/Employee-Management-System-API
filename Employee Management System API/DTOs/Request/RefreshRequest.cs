namespace Employee_Management_System_API.DTOs.Request
{
    public class RefreshRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
    }
}
