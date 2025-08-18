using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Request
{
    public class InsertAccountRequest : InsertEmployeeRequest
    {
        [Required]
        [DisplayName("Account name")]
        public string UserName { get; set; } = default!;

        [Required]
        [DisplayName("Employee Position")]
        public OrganizationRoles OrgRole { get; set; }

        [Required]
        [DisplayName("Account password")]
        public string Password { get; set; } = default!;
    }
}
