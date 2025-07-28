using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Response
{
    public class EmployeeResponse
    {
        public string EmployeePub_ID { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string MiddleName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; }
        public DateOnly HireDate { get; set; }
        public string Address { get; set; } = default!;
        public EmployeeStatus Status { get; set; }
        public string DepartmentPub_ID { get; set; } = default!;
        public string RolePub_ID { get; set; } = default!;
    }
}
