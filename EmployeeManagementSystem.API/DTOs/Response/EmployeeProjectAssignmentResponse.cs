using Employee_Management_System_API.DTOs.Response.Employee;

namespace Employee_Management_System_API.DTOs.Response
{
    public class EmployeeProjectAssignmentResponse
    {
        public string EmployeePub_ID { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string MiddleName { get; set; } = default!;

        public string LastName { get; set; } = default!;
        public IEnumerable<ForEmployeeProjectAssignmentResponse> ProjectAssignments { get; set; } =
                                                        new List<ForEmployeeProjectAssignmentResponse>();
    }
}
