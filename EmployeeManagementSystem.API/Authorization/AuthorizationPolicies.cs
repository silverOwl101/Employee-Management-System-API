using Microsoft.AspNetCore.Authorization;

namespace Employee_Management_System_API.Authorization
{
    public static class AuthorizationPolicies
    {
        public static void SystemPolicies(AuthorizationOptions options)
        {
            //Account Controller Policy
            options.AddPolicy("Account.Register", policy => policy.RequireClaim("Account.Register", "true"));

            //Attendance Controller Policy
            options.AddPolicy("Attendance.View", policy => policy.RequireClaim("Attendance.View", "true"));
            options.AddPolicy("Attendance.ById", policy => policy.RequireClaim("Attendance.ById", "true"));
            options.AddPolicy("Attendance.Create", policy => policy.RequireClaim("Attendance.Create", "true"));
            options.AddPolicy("Attendance.Update", policy => policy.RequireClaim("Attendance.Update", "true"));
            options.AddPolicy("Attendance.Delete", policy => policy.RequireClaim("Attendance.Delete", "true"));

            //Department Controller Policy
            options.AddPolicy("Department.View", policy => policy.RequireClaim("Department.View", "true"));
            options.AddPolicy("Department.ById", policy => policy.RequireClaim("Department.ById", "true"));
            options.AddPolicy("Department.Create", policy => policy.RequireClaim("Department.Create", "true"));
            options.AddPolicy("Department.Update", policy => policy.RequireClaim("Department.Update", "true"));
            options.AddPolicy("Department.Delete", policy => policy.RequireClaim("Department.Delete", "true"));

            //Employee Controller Policy
            options.AddPolicy("Employee.View", policy => policy.RequireClaim("Employee.View", "true"));
            options.AddPolicy("Employee.ById", policy => policy.RequireClaim("Employee.ById", "true"));
            options.AddPolicy("Employee.Attendance", policy => policy.RequireClaim("Employee.Attendance", "true"));
            options.AddPolicy("Employee.LeaveRequest", policy => policy.RequireClaim("Employee.LeaveRequest", "true"));
            options.AddPolicy("Employee.Payroll", policy => policy.RequireClaim("Employee.Payroll", "true"));
            options.AddPolicy("Employee.PerformanceReview", policy =>
                                                            policy.RequireClaim("Employee.PerformanceReview", "true"));
            options.AddPolicy("Employee.PhoneNumbers", policy => policy.RequireClaim("Employee.PhoneNumbers", "true"));
            options.AddPolicy("Employee.ProjectAssignment", policy =>
                                                            policy.RequireClaim("Employee.ProjectAssignment", "true"));
            options.AddPolicy("Employee.Create", policy => policy.RequireClaim("Employee.Create", "true"));
            options.AddPolicy("Employee.Update", policy => policy.RequireClaim("Employee.Update", "true"));
            options.AddPolicy("Employee.Delete", policy => policy.RequireClaim("Employee.Delete", "true"));

            //LeaveRequest Controller Policy
            options.AddPolicy("LeaveRequest.View", policy => policy.RequireClaim("LeaveRequest.View", "true"));
            options.AddPolicy("LeaveRequest.ById", policy => policy.RequireClaim("LeaveRequest.ById", "true"));
            options.AddPolicy("LeaveRequest.Create", policy => policy.RequireClaim("LeaveRequest.Create", "true"));
            options.AddPolicy("LeaveRequest.Update", policy => policy.RequireClaim("LeaveRequest.Update", "true"));
            options.AddPolicy("LeaveRequest.Delete", policy => policy.RequireClaim("LeaveRequest.Delete", "true"));

            //Payroll Controller Policy
            options.AddPolicy("Payroll.View", policy => policy.RequireClaim("Payroll.View", "true"));
            options.AddPolicy("Payroll.ById", policy => policy.RequireClaim("Payroll.ById", "true"));
            options.AddPolicy("Payroll.Create", policy => policy.RequireClaim("Payroll.Create", "true"));
            options.AddPolicy("Payroll.Update", policy => policy.RequireClaim("Payroll.Update", "true"));
            options.AddPolicy("Payroll.Delete", policy => policy.RequireClaim("Payroll.Delete", "true"));

            //PerformanceReview Controller Policy
            options.AddPolicy("PerformanceReview.View", policy =>
                             policy.RequireClaim("PerformanceReview.View", "true"));
            options.AddPolicy("PerformanceReview.ById", policy =>
                             policy.RequireClaim("PerformanceReview.ById", "true"));
            options.AddPolicy("PerformanceReview.Create", policy =>
                             policy.RequireClaim("PerformanceReview.Create", "true"));
            options.AddPolicy("PerformanceReview.Update", policy =>
                             policy.RequireClaim("PerformanceReview.Update", "true"));
            options.AddPolicy("PerformanceReview.Delete", policy =>
                             policy.RequireClaim("PerformanceReview.Delete", "true"));

            //PhoneNumber Controller Policy
            options.AddPolicy("PhoneNumber.View", policy => policy.RequireClaim("PhoneNumber.View", "true"));
            options.AddPolicy("PhoneNumber.ById", policy => policy.RequireClaim("PhoneNumber.ById", "true"));
            options.AddPolicy("PhoneNumber.Create", policy => policy.RequireClaim("PhoneNumber.Create", "true"));
            options.AddPolicy("PhoneNumber.Update", policy => policy.RequireClaim("PhoneNumber.Update", "true"));
            options.AddPolicy("PhoneNumber.Delete", policy => policy.RequireClaim("PhoneNumber.Delete", "true"));

            //ProjectAssignment Controller Policy
            options.AddPolicy("ProjectAssignment.View", policy =>
                             policy.RequireClaim("ProjectAssignment.View", "true"));
            options.AddPolicy("ProjectAssignment.ById", policy =>
                             policy.RequireClaim("ProjectAssignment.ById", "true"));
            options.AddPolicy("ProjectAssignment.Create", policy =>
                             policy.RequireClaim("ProjectAssignment.Create", "true"));
            options.AddPolicy("ProjectAssignment.Update", policy =>
                             policy.RequireClaim("ProjectAssignment.Update", "true"));
            options.AddPolicy("ProjectAssignment.Delete", policy =>
                             policy.RequireClaim("ProjectAssignment.Delete", "true"));

            //Project Controller Policy
            options.AddPolicy("Project.View", policy => policy.RequireClaim("Project.View", "true"));
            options.AddPolicy("Project.ById", policy => policy.RequireClaim("Project.ById", "true"));
            options.AddPolicy("Project.GetEmployees", policy => policy.RequireClaim("Project.GetEmployees", "true"));
            options.AddPolicy("Project.Create", policy => policy.RequireClaim("Project.Create", "true"));
            options.AddPolicy("Project.Update", policy => policy.RequireClaim("Project.Update", "true"));
            options.AddPolicy("Project.Delete", policy => policy.RequireClaim("Project.Delete", "true"));

            //Role Controller Policy
            options.AddPolicy("Role.View", policy => policy.RequireClaim("Role.View", "true"));
            options.AddPolicy("Role.ById", policy => policy.RequireClaim("Role.ById", "true"));
            options.AddPolicy("Role.Create", policy => policy.RequireClaim("Role.Create", "true"));
            options.AddPolicy("Role.Update", policy => policy.RequireClaim("Role.Update", "true"));
            options.AddPolicy("Role.Delete", policy => policy.RequireClaim("Role.Delete", "true"));
        }
    }
}
