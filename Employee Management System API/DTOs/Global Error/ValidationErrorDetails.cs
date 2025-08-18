namespace Employee_Management_System_API.DTOs.Global_Error
{
    public class ValidationErrorDetails
    {
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public int Status { get; set; }
        public string Error { get; set; } = string.Empty;
        public Dictionary<string, string[]>? Messages { get; set; }
        public string? Path { get; set; }
        public string? TraceId { get; set; }
    }
}
