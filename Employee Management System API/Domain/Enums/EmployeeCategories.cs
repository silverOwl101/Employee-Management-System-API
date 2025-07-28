namespace Employee_Management_System_API.Domain.Enums
{
    public class EmployeeCategories
    {
        public enum SortGetAllAsync
        {
            FirstName,
            MiddleName,
            LastName,
            Email,
            DateOfBirth,
            HireDate,
            Address,
            Status,
        }

        public enum SortGetAttendancesAsync
        {
            AttendancePub_ID,
            Date,
            CheckInTime,
            CheckOutTime
        }

        public enum SortGetLeaveRequestsAsync
        {
            LeavePub_ID,
            StartDate,
            EndDate,
            LeaveType,
            Status
        }

        public enum SortGetPayrollsAsync
        {
            PayrollPub_ID,
            PayDate
        }

        public enum SortGetProjectAssignmentsAsync
        {
            AssignmentPub_ID,
            RoleInProject,
            AssignedDate
        }

        public enum SortGetPerformanceReviewsAsync
        {
            ReviewPub_ID,
            ReviewDate,
            Score
        }

        public enum SortGetPhoneNumbersAsync
        {
            PhoneNumberPub_ID,
            PhoneNumberValue
        }
    }
}
