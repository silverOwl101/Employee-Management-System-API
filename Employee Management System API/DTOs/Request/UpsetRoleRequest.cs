using Employee_Management_System_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsetRoleRequest
    {                
        [Required, MaxLength(10, ErrorMessage = "RoleID cannot be over 10 over characters")]
        [DisplayName("Role ID")]
        public string RolePub_ID { get; set; } = default!;

        [Required, MaxLength(100, ErrorMessage = "Role name cannot be over 100 over characters")]
        [DisplayName("Role name")]
        public string RoleName { get; set; } = default!;

        [DisplayName("Role description")]
        public string? Description { get; set; }
        
        [Required, Precision(10, 2)]
        [DisplayName("Employee salary")]
        public decimal Salary { get; set; }     
    }
}
