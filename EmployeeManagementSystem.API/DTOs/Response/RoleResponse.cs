using Employee_Management_System_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Response
{
    public class RoleResponse
    {                     
        public string RolePub_ID { get; set; } = default!;
        
        public string RoleName { get; set; } = default!;

        public string? Description { get; set; }
        
        public decimal Salary { get; set; }

        
    }
}
