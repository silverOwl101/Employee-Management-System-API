namespace Employee_Management_System_API.DTOs.Response.Employee
{
    public class ForEmployeeProjectAssignmentResponse
    {
        public string AssignmentPub_ID { get; set; } = default!;
        public string ProjectName { get; set; } = default!;
        public string RoleInProject { get; set; } = default!;
        public DateOnly AssignedDate { get; set; }
    }
}
