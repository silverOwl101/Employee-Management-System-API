namespace Employee_Management_System_API.Domain.Enums
{
    public class Categories
    {
        public enum EmployeeStatus
        {
            Active,
            OnLeave,
            Resigned,
            Terminated
        }
        public enum AttendanceStatus
        {
            Present,
            Absent,
            Remote,
            Leave
        }
        public enum LeaveType
        {
            Sick,
            Casual,
            Paid,
            Unpaid,
            Maternity,
            Paternity,
            Vacation
        }
        public enum LeaveStatus
        {
            Pending,
            Approved,
            Rejected
        }
        public enum ProjectStatus
        {
            Ongoing,
            Completed,
            OnHold
        }

        public enum OrganizationRoles
        {
            HRManager,
            TeamLead,
            PayrollOfficer,
            HRStaff,
            Intern,
            PayrollAssistant,
            ProjectCoordinator,
            LeaveCoordinator,
            ProjectLead,
            ProjectManager,
            Employee,
            DepartmentHead,
            LeaveApprover,
            HRAssistant
        }

    }
}
