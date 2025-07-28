using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Response
{
    public class EmployeeAssignedResponse
    {
        public string EmployeePub_ID { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string MiddleName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string RoleName { get; set; } = default!;
        public string RoleInProject { get; set; } = default!;
        public string DepartmentName { get; set; } = default!;
        public EmployeeStatus Status { get; set; }
    }
}
