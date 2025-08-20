namespace Employee_Management_System_API.DTOs.Response
{
    public class ProjectAssignmentResponse
    {
        public string AssignmentPub_ID { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string MiddleName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string ProjectName { get; set; } = default!;
        public string RoleInProject { get; set; } = default!;
        public DateOnly AssignedDate { get; set; }

    }
}