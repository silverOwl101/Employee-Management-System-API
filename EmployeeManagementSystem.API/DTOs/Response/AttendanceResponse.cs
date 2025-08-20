using Employee_Management_System_API.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Response
{
    public class AttendanceResponse
    {
        public string AttendancePub_ID { get; set; } = default!;
        public DateOnly Date { get; set; }
        
        public TimeSpan CheckInTime { get; set; }

        public TimeSpan CheckOutTime { get; set; }

        public AttendanceStatus Status { get; set; }
    }
}
