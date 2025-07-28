using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Request
{
    public class InsertAccountRequest : InsertEmployeeRequest
    {
        [Required]
        public string UserName { get; set; } = default!;
        
        [Required]
        public OrganizationRoles OrgRole { get; set; }

        [Required]
        public string Password { get; set; } = default!;
    }
}
