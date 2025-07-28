using System.ComponentModel.DataAnnotations;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.DTOs.Response
{
    public class LeaveRequestResponse
    {
        public string LeavePub_ID { get; set; } = default!;

        public DateOnly StartDate { get; set; }
        
        public DateOnly EndDate { get; set; }

        public LeaveType LeaveType { get; set; }

        public LeaveStatus Status { get; set; }

        public string Reason { get; set; } = default!;
    }
}