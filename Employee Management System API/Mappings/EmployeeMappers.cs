using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.DTOs.Response.Employee;

namespace Employee_Management_System_API.Mappings
{
    public static class EmployeeMappers
    {
        public static EmployeeResponse ToEmployeeResponse(this Employee employee)
        {
            return new EmployeeResponse
            {
                EmployeePub_ID = employee.EmployeePub_ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Email = employee.Email,
                DateOfBirth = employee.DateOfBirth,
                HireDate = employee.HireDate,
                Address = employee.Address,
                Status = employee.Status,
                DepartmentPub_ID = employee.Department.DepartmentPub_ID,
                RolePub_ID = employee.Role.RolePub_ID
            };
        }
        public static EmployeeAssignedResponse ToEmployeeProjectAssigned(this Employee employee)
        {
            return new EmployeeAssignedResponse
            {
                EmployeePub_ID = employee.EmployeePub_ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                RoleName = employee.Role.RoleName,
                DepartmentName = employee.Department.DepartmentName,
                Status = employee.Status,
            };
        }
        public static EmployeeAttendanceResponse ToEmployeeAttendanceResponse(this Employee employee)
        {
            return new EmployeeAttendanceResponse
            {
                EmployeePub_ID = employee.EmployeePub_ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Attendances = employee.Attendances.Select(e => new AttendanceResponse
                {
                    AttendancePub_ID = e.AttendancePub_ID,
                    Date = e.Date,
                    CheckInTime = e.CheckInTime,
                    CheckOutTime = e.CheckOutTime,
                    Status = e.Status
                })
            };
        }
        public static EmployeeLeaveResponse ToEmployeeLeaveResponse(this Employee employee)
        {
            return new EmployeeLeaveResponse
            {
                EmployeePub_ID = employee.EmployeePub_ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                LeaveRequests = employee.LeaveRequests.Select(e => new LeaveRequestResponse
                {
                    LeavePub_ID = e.LeavePub_ID,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    LeaveType = e.LeaveType,
                    Status = e.Status,
                    Reason = e.Reason
                })
            };
        }
        public static EmployeePayrollResponse ToEmployeePayrollResponse(this Employee employee)
        {
            return new EmployeePayrollResponse
            {
                EmployeePub_ID = employee.EmployeePub_ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Payrolls = employee.Payrolls.Select(e => new PayrollResponse
                {
                    PayrollPub_ID = e.PayrollPub_ID,
                    PayDate = e.PayDate,
                    BasicSalary = e.BasicSalary,
                    Allowances = e.Allowances,
                    Deductions = e.Deductions,
                    NetSalary = e.NetSalary
                })
            };
        }
        public static EmployeeProjectAssignmentResponse ToEmployeeProjectAssignmentResponse(this Employee employee)
        {
            return new EmployeeProjectAssignmentResponse
            {
                EmployeePub_ID = employee.EmployeePub_ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                ProjectAssignments = employee.ProjectAssignments.Select(e => new ForEmployeeProjectAssignmentResponse
                {
                    AssignmentPub_ID = e.AssignmentPub_ID,
                    ProjectName = e.Project.ProjectName,
                    RoleInProject = e.RoleInProject,
                    AssignedDate = e.AssignedDate
                })
            };
        }
        public static EmployeePerformanceReviewResponse ToEmployeePerformanceReviewResponse(this Employee employee)
        {
            return new EmployeePerformanceReviewResponse
            {
                EmployeePub_ID = employee.EmployeePub_ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                PerformanceReviews = employee.PerformanceReviews.Select(e => new PerformanceReviewResponse
                {
                    ReviewPub_ID = e.ReviewPub_ID,
                    ReviewDate = e.ReviewDate,
                    Score = e.Score,
                    Comments = e.Comments
                })
            };
        }
        public static EmployeePhoneNumberResponse ToPhoneNumbersResponse(this Employee employee)
        {
            return new EmployeePhoneNumberResponse
            {
                EmployeePub_ID = employee.EmployeePub_ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                PhoneNumbers = employee.PhoneNumbers.Select(e => new PhoneNumberResponse
                {
                    PhoneNumberPub_ID = e.PhoneNumberPub_ID,
                    PhoneNumberValue = e.PhoneNumberValue
                })
            };
        }
    }
}
