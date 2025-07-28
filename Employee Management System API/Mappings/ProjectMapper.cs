using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Response;

namespace Employee_Management_System_API.Mappings
{
    public static class ProjectMapper
    {
        public static ProjectResponse ToProjectDto(this Project project)
        {
            return new ProjectResponse
            {
                ProjectPub_ID = project.ProjectPub_ID,
                ProjectName = project.ProjectName,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status
            };
        }
    }
}
