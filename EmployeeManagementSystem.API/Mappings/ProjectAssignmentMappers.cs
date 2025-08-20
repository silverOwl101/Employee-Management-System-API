using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Response;

namespace Employee_Management_System_API.Mappings
{
    public static class ProjectAssignmentMappers
    {
        public static ProjectAssignmentResponse ToProjectAssignmentDto(this ProjectAssignment projectAssignment)
        {
            return new ProjectAssignmentResponse
            {
                AssignmentPub_ID = projectAssignment.AssignmentPub_ID,
                FirstName = projectAssignment.Employee.FirstName,
                MiddleName = projectAssignment.Employee.MiddleName,
                LastName = projectAssignment.Employee.LastName,
                ProjectName = projectAssignment.Project.ProjectName,
                RoleInProject = projectAssignment.RoleInProject,
                AssignedDate = projectAssignment.AssignedDate
            };
        }

        public static ProjectAssignmentResponse ToEmployeeProjectAssignmentDto(this ProjectAssignment projectAssignment)
        {
            return new ProjectAssignmentResponse
            {
                AssignmentPub_ID = projectAssignment.AssignmentPub_ID,
                ProjectName = projectAssignment.Project.ProjectName,
                RoleInProject = projectAssignment.RoleInProject,
                AssignedDate = projectAssignment.AssignedDate
            };
        }
    }
}
