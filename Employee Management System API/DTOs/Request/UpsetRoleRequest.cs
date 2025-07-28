using Employee_Management_System_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsetRoleRequest
    {                
        [Required, MaxLength(10, ErrorMessage = "RoleID cannot be over 10 over characters")]
        public string RolePub_ID { get; set; } = default!;

        [Required, MaxLength(100, ErrorMessage = "Role name cannot be over 100 over characters")]
        public string RoleName { get; set; } = default!;

        public string? Description { get; set; }

        [Required, Precision(10, 2)]
        public decimal Salary { get; set; }     
    }
}
