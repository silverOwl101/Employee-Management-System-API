using Employee_Management_System_API.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Response
{
    public class EmployeeAttendanceResponse
    {
        public string EmployeePub_ID { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string MiddleName { get; set; } = default!;

        public string LastName { get; set; } = default!;
        public IEnumerable<AttendanceResponse> Attendances { get; set; } = new List<AttendanceResponse>();
    }
}
