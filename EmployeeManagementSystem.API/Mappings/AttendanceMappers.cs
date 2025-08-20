using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Response;

namespace Employee_Management_System_API.Mappings
{
    public static class AttendanceMappers
    {
        public static AttendanceResponse ToAttendanceDto(this Attendance attendance)
        {
            return new AttendanceResponse
            {
                AttendancePub_ID = attendance.AttendancePub_ID,
                Date = attendance.Date,
                CheckInTime = attendance.CheckInTime,
                CheckOutTime = attendance.CheckOutTime,
                Status = attendance.Status
            };
        }
    }
}
