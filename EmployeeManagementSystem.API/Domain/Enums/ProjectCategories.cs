namespace Employee_Management_System_API.Domain.Enums
{
    public class ProjectCategories
    {
        public enum SortByGetAllProject
        {
            ProjectPub_ID,
            ProjectName,
            StartDate,
            EndDate,
            Status
        }

        public enum SortByGetById
        {
            EmployeePub_ID,
            FirstName,
            MiddleName,
            LastName,
            RoleName,
            RoleInProject,
            DepartmentName,
            Status
        }
    }
}
