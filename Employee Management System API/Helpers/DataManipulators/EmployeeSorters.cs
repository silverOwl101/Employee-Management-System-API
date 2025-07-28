using Employee_Management_System_API.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Linq.Expressions;
using static Employee_Management_System_API.Domain.Enums.EmployeeCategories;

namespace Employee_Management_System_API.Helpers.DataManipulators
{
    public static class EmployeeSorters
    {
        public static IEnumerable<T> Sort<T>(IQueryable<T> query, string sortValue, bool isDesc)
        {
            var parameter = Expression.Parameter(typeof(T), "x");   
            var property = Expression.Property(parameter, sortValue);
            var lambda = Expression.Lambda(property, parameter);

            string linqMethodName = isDesc ? "OrderByDescending" : "OrderBy";
            var result = typeof(Queryable).GetMethods().First(a => a.Name == linqMethodName && a.GetParameters().Length == 2)
                                          .MakeGenericMethod(typeof(T), property.Type)
                                          .Invoke(null, new object[] { query, lambda });
            return (IEnumerable<T>)result!;
        }

        public static IQueryable<Employee> ApplySortingGetAllAsync(IQueryable<Employee> query, SortGetAllAsync? sortEnums, bool isDesc)
        {            
            if (!sortEnums.HasValue) return query;

            return sortEnums switch
            {
                SortGetAllAsync.FirstName => isDesc ? query.OrderByDescending(e => e.FirstName) : query.OrderBy(e => e.FirstName),
                SortGetAllAsync.MiddleName => isDesc ? query.OrderByDescending(e => e.MiddleName) : query.OrderBy(e => e.MiddleName),
                SortGetAllAsync.LastName => isDesc ? query.OrderByDescending(e => e.LastName) : query.OrderBy(e => e.LastName),
                SortGetAllAsync.Email => isDesc ? query.OrderByDescending(e => e.Email) : query.OrderBy(e => e.Email),
                SortGetAllAsync.DateOfBirth => isDesc ? query.OrderByDescending(e => e.DateOfBirth) : query.OrderBy(e => e.DateOfBirth),
                SortGetAllAsync.HireDate => isDesc ? query.OrderByDescending(e => e.HireDate) : query.OrderBy(e => e.HireDate),
                SortGetAllAsync.Address => isDesc ? query.OrderByDescending(e => e.Address) : query.OrderBy(e => e.Address),
                SortGetAllAsync.Status => isDesc ? query.OrderByDescending(e => e.Status) : query.OrderBy(e => e.Status),
                _ => query
            };
        }
        public static IQueryable<Attendance> ApplySortingGetAttendancesAsync(IQueryable<Attendance> query, SortGetAttendancesAsync? sortEnums, bool isDesc)
        {
            if (!sortEnums.HasValue) return query;

            return sortEnums switch
            {
                SortGetAttendancesAsync.AttendancePub_ID => isDesc ? query.OrderByDescending(e => e.AttendancePub_ID) : query.OrderBy(e => e.AttendancePub_ID),
                SortGetAttendancesAsync.Date => isDesc ? query.OrderByDescending(e => e.Date) : query.OrderBy(e => e.Date),
                SortGetAttendancesAsync.CheckInTime => isDesc ? query.OrderByDescending(e => e.CheckInTime) : query.OrderBy(e => e.CheckInTime),
                SortGetAttendancesAsync.CheckOutTime => isDesc ? query.OrderByDescending(e => e.CheckOutTime) : query.OrderBy(e => e.CheckOutTime),

                _ => query
            };
        }
        public static IQueryable<LeaveRequest> ApplySortingGetLeaveRequestsAsync(IQueryable<LeaveRequest> query, SortGetLeaveRequestsAsync? sortEnums, bool isDesc)
        {
            if (!sortEnums.HasValue) return query;

            return sortEnums switch
            {
                SortGetLeaveRequestsAsync.LeavePub_ID => isDesc ? query.OrderByDescending(e => e.LeavePub_ID) : query.OrderBy(e => e.LeavePub_ID),
                SortGetLeaveRequestsAsync.StartDate => isDesc ? query.OrderByDescending(e => e.StartDate) : query.OrderBy(e => e.StartDate),
                SortGetLeaveRequestsAsync.EndDate => isDesc ? query.OrderByDescending(e => e.EndDate) : query.OrderBy(e => e.EndDate),
                SortGetLeaveRequestsAsync.LeaveType => isDesc ? query.OrderByDescending(e => e.LeaveType) : query.OrderBy(e => e.LeaveType),
                SortGetLeaveRequestsAsync.Status => isDesc ? query.OrderByDescending(e => e.Status) : query.OrderBy(e => e.Status),
                _ => query
            };
        }
        public static IQueryable<Payroll> ApplySortingGetLeaveRequestsAsync(IQueryable<Payroll> query, SortGetPayrollsAsync? sortEnums, bool isDesc)
        {
            if (!sortEnums.HasValue) return query;

            return sortEnums switch
            {
                SortGetPayrollsAsync.PayrollPub_ID => isDesc ? query.OrderByDescending(e => e.PayrollPub_ID) : query.OrderBy(e => e.PayrollPub_ID),
                SortGetPayrollsAsync.PayDate => isDesc ? query.OrderByDescending(e => e.PayDate) : query.OrderBy(e => e.PayDate),
                _ => query
            };
        }
        public static IQueryable<ProjectAssignment> ApplySortingGetProjectAssignmentsAsync(IQueryable<ProjectAssignment> query, SortGetProjectAssignmentsAsync? sortEnums, bool isDesc)
        {
            if (!sortEnums.HasValue) return query;

            return sortEnums switch
            {
                SortGetProjectAssignmentsAsync.AssignmentPub_ID => isDesc ? query.OrderByDescending(e => e.AssignmentPub_ID) : query.OrderBy(e => e.AssignmentPub_ID),
                SortGetProjectAssignmentsAsync.RoleInProject => isDesc ? query.OrderByDescending(e => e.RoleInProject) : query.OrderBy(e => e.RoleInProject),
                SortGetProjectAssignmentsAsync.AssignedDate => isDesc ? query.OrderByDescending(e => e.AssignedDate) : query.OrderBy(e => e.AssignedDate),
                _ => query
            };
        }
        public static IQueryable<PerformanceReview> ApplySortingGetPerformanceReviewsAsync(IQueryable<PerformanceReview> query, SortGetPerformanceReviewsAsync? sortEnums, bool isDesc)
        {
            if (!sortEnums.HasValue) return query;

            return sortEnums switch
            {
                SortGetPerformanceReviewsAsync.ReviewPub_ID => isDesc ? query.OrderByDescending(e => e.ReviewPub_ID) : query.OrderBy(e => e.ReviewPub_ID),
                SortGetPerformanceReviewsAsync.ReviewDate => isDesc ? query.OrderByDescending(e => e.ReviewDate) : query.OrderBy(e => e.ReviewDate),
                SortGetPerformanceReviewsAsync.Score => isDesc ? query.OrderByDescending(e => e.Score) : query.OrderBy(e => e.Score),
                _ => query
            };
        }
        public static IQueryable<PhoneNumber> ApplySortingGetPhoneNumbersAsync(IQueryable<PhoneNumber> query, SortGetPhoneNumbersAsync? sortEnums, bool isDesc)
        {
            if (!sortEnums.HasValue) return query;

            return sortEnums switch
            {
                SortGetPhoneNumbersAsync.PhoneNumberPub_ID => isDesc ? query.OrderByDescending(e => e.PhoneNumberPub_ID) : query.OrderBy(e => e.PhoneNumberPub_ID),
                SortGetPhoneNumbersAsync.PhoneNumberValue => isDesc ? query.OrderByDescending(e => e.PhoneNumberValue) : query.OrderBy(e => e.PhoneNumberValue),
                _ => query
            };
        }
    }
}
