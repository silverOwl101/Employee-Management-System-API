using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Response;

namespace Employee_Management_System_API.Mappings
{
    public static class DepartmentMappers
    {
        public static DepartmentResponse ToDepartmentDto(this Department department)
        {
            return new DepartmentResponse
            {
                DepartmentPub_ID = department.DepartmentPub_ID,
                DepartmentName = department.DepartmentName,
                Description = department.Description
            };
        }
    }
}
