namespace Employee_Management_System_API.DTOs.Global_Error
{
    public class ErrorDetails
    {
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public int Status { get; set; }
        public string Error { get; set; } = string.Empty;
        public string? Message { get; set; }
        public string? Path { get; set; }
        public string? TraceId { get; set; }
    }
}
