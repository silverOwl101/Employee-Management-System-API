namespace Employee_Management_System_API.DTOs.Response
{
    public class DepartmentResponse
    {
        public string DepartmentPub_ID { get; set; } = default!;
        public string DepartmentName { get; set; } = default!;
        public string? Description { get; set; }
    }
}
