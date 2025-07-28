using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Helpers.DataManipulators;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Queries.Employee;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDBContext _context;
        public EmployeeRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Employee> CreateAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return await Task.FromResult(employee);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeePub_ID == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return true; // Employee deleted successfully
            }
            return false; // Employee not found
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(QueryGetAllEmployees employeeQuery)
        {
            var listOfEmployees = _context.Employees.Include(e => e.Department)
                                                    .Include(e => e.Role)
                                                    .AsNoTracking()
                                                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(employeeQuery.FirstName))
                listOfEmployees = listOfEmployees.Where(q => q.FirstName.Contains(employeeQuery.FirstName));

            if (!string.IsNullOrWhiteSpace(employeeQuery.MiddleName))
                listOfEmployees = listOfEmployees.Where(q => q.MiddleName.Contains(employeeQuery.MiddleName));

            if (!string.IsNullOrWhiteSpace(employeeQuery.LastName))
                listOfEmployees = listOfEmployees.Where(q => q.LastName.Contains(employeeQuery.LastName));

            if (!string.IsNullOrWhiteSpace(employeeQuery.Email))
                listOfEmployees = listOfEmployees.Where(q => q.Email.Contains(employeeQuery.Email));

            if (employeeQuery.DateOfBirth.HasValue)
                listOfEmployees = listOfEmployees.Where(q => q.DateOfBirth == employeeQuery.DateOfBirth);

            if (employeeQuery.HireDate.HasValue)
                listOfEmployees = listOfEmployees.Where(q => q.HireDate == employeeQuery.HireDate);

            if (!string.IsNullOrWhiteSpace(employeeQuery.Address))
                listOfEmployees = listOfEmployees.Where(q => q.Address.Contains(employeeQuery.Address));

            if (employeeQuery.Status.HasValue)
                listOfEmployees = listOfEmployees.Where(q => q.Status == employeeQuery.Status);

            if (employeeQuery.Sortby.HasValue)
                listOfEmployees = EmployeeSorters.ApplySortingGetAllAsync(listOfEmployees, employeeQuery.Sortby, employeeQuery.IsDecsending);

            return await EmployeePagination.Pagination(listOfEmployees, employeeQuery.PageNumber, employeeQuery.PageSize).ToListAsync();
        }

        public async Task<Employee?> GetAttendancesAsync(QueryGetEmployeeAttendance employeeQuery)
        {
            var query = _context.Employees.Include(e => e.Attendances).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(employeeQuery.EmployeePub_ID))
                query = query.Where(e => e.EmployeePub_ID == employeeQuery.EmployeePub_ID); // filter employee using EmployeePub_ID

            var employeeInformation = await query.FirstOrDefaultAsync(); // get the employee model properties

            if (employeeInformation is not null)
            {
                var attendanceQuery = employeeInformation.Attendances.AsQueryable();

                if (!string.IsNullOrEmpty(employeeQuery.AttendancePub_ID))
                    attendanceQuery = attendanceQuery.Where(e => e.AttendancePub_ID == employeeQuery.AttendancePub_ID);

                if (employeeQuery.Date.HasValue)
                    attendanceQuery = attendanceQuery.Where(e => e.Date == employeeQuery.Date);

                if (employeeQuery.CheckInTime.HasValue)
                    attendanceQuery = attendanceQuery.Where(e => e.CheckInTime == employeeQuery.CheckInTime);

                if (employeeQuery.CheckOutTime.HasValue)
                    attendanceQuery = attendanceQuery.Where(e => e.CheckOutTime == employeeQuery.CheckOutTime);

                if (employeeQuery.Sortby.HasValue)
                    attendanceQuery = EmployeeSorters.ApplySortingGetAttendancesAsync(attendanceQuery, employeeQuery.Sortby, employeeQuery.IsDecsending);

                var attendance = EmployeePagination.Pagination(attendanceQuery, employeeQuery.PageNumber, employeeQuery.PageSize).ToList();
                employeeInformation.Attendances = attendance;
            }

            return employeeInformation;
        }

        public async Task<Employee?> GetLeaveRequestsAsync(QueryGetEmployeeLeaveRequest employeeQuery)
        {
            var query = _context.Employees.Include(e => e.LeaveRequests).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(employeeQuery.EmployeePub_ID))
                query = query.Where(e => e.EmployeePub_ID == employeeQuery.EmployeePub_ID); // filter employee using EmployeePub_ID

            var employeeInformation = await query.FirstOrDefaultAsync(); // get the employee model properties

            if (employeeInformation is not null)
            {
                var leaveRequestQuery = employeeInformation.LeaveRequests.AsQueryable();

                if (!string.IsNullOrEmpty(employeeQuery.LeavePub_ID))
                    leaveRequestQuery = leaveRequestQuery.Where(e => e.LeavePub_ID == employeeQuery.LeavePub_ID);

                if (employeeQuery.StartDate.HasValue)
                    leaveRequestQuery = leaveRequestQuery.Where(e => e.StartDate == employeeQuery.StartDate);

                if (employeeQuery.EndDate.HasValue)
                    leaveRequestQuery = leaveRequestQuery.Where(e => e.EndDate == employeeQuery.EndDate);

                if (employeeQuery.LeaveType.HasValue)
                    leaveRequestQuery = leaveRequestQuery.Where(e => e.LeaveType == employeeQuery.LeaveType);

                if (employeeQuery.Status.HasValue)
                    leaveRequestQuery = leaveRequestQuery.Where(e => e.Status == employeeQuery.Status);

                if (employeeQuery.Sortby.HasValue)
                    leaveRequestQuery = EmployeeSorters.ApplySortingGetLeaveRequestsAsync(leaveRequestQuery, employeeQuery.Sortby, employeeQuery.IsDecsending);

                var filteredLeaveRequest = EmployeePagination.Pagination(leaveRequestQuery,
                                                                         employeeQuery.PageNumber,
                                                                         employeeQuery.PageSize).ToList();
                employeeInformation.LeaveRequests = filteredLeaveRequest;
            }

            return employeeInformation;
        }

        public async Task<Employee?> GetPayrollsAsync(QueryGetEmployeePayroll employeeQuery)
        {
            var query = _context.Employees.Include(e => e.Payrolls).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(employeeQuery.EmployeePub_ID))
                query = query.Where(e => e.EmployeePub_ID == employeeQuery.EmployeePub_ID); // filter employee using EmployeePub_ID

            var employeeInformation = await query.FirstOrDefaultAsync(); // get the employee model properties

            if (employeeInformation is not null)
            {
                var payrollRequestQuery = employeeInformation.Payrolls.AsQueryable();

                if (!string.IsNullOrEmpty(employeeQuery.PayrollPub_ID))
                    payrollRequestQuery = payrollRequestQuery.Where(e => e.PayrollPub_ID == employeeQuery.PayrollPub_ID);

                if (employeeQuery.PayDate.HasValue)
                    payrollRequestQuery = payrollRequestQuery.Where(e => e.PayDate == employeeQuery.PayDate);

                if (employeeQuery.Sortby.HasValue)
                    payrollRequestQuery = EmployeeSorters.ApplySortingGetLeaveRequestsAsync(payrollRequestQuery, employeeQuery.Sortby, employeeQuery.IsDecsending);

                var filteredLeaveRequest = EmployeePagination.Pagination(payrollRequestQuery,
                                                                         employeeQuery.PageNumber,
                                                                         employeeQuery.PageSize).ToList();
                employeeInformation.Payrolls = filteredLeaveRequest;
            }

            return employeeInformation;
        }

        public async Task<Employee?> GetPerformanceReviewsAsync(QueryGetPerformanceReviewsAsync employeeQuery)
        {
            var query = _context.Employees.Include(e => e.PerformanceReviews).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(employeeQuery.EmployeePub_ID))
                query = query.Where(e => e.EmployeePub_ID == employeeQuery.EmployeePub_ID); // filter employee using EmployeePub_ID

            var employeeInformation = await query.FirstOrDefaultAsync(); // get the employee model properties

            if (employeeInformation is not null)
            {
                var performanceReviewQuery = employeeInformation.PerformanceReviews.AsQueryable();

                if (!string.IsNullOrEmpty(employeeQuery.ReviewPub_ID))
                    performanceReviewQuery = performanceReviewQuery.Where(e => e.ReviewPub_ID == employeeQuery.ReviewPub_ID);

                if (employeeQuery.ReviewDate.HasValue)
                    performanceReviewQuery = performanceReviewQuery.Where(e => e.ReviewDate == employeeQuery.ReviewDate);

                if (employeeQuery.Score.HasValue)
                    performanceReviewQuery = performanceReviewQuery.Where(e => e.Score == employeeQuery.Score);

                if (employeeQuery.Sortby.HasValue)
                    performanceReviewQuery = EmployeeSorters.ApplySortingGetPerformanceReviewsAsync(performanceReviewQuery, employeeQuery.Sortby, employeeQuery.IsDecsending);

                var filteredLeaveRequest = EmployeePagination.Pagination(performanceReviewQuery,
                                                                         employeeQuery.PageNumber,
                                                                         employeeQuery.PageSize).ToList();
                employeeInformation.PerformanceReviews = filteredLeaveRequest;
            }
            return employeeInformation;
        }

        public async Task<Employee?> GetPhoneNumbersAsync(QueryGetPhoneNumbersAsync employeeQuery)
        {
            var query = _context.Employees.Include(e => e.PhoneNumbers).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(employeeQuery.EmployeePub_ID))
                query = query.Where(e => e.EmployeePub_ID == employeeQuery.EmployeePub_ID); // filter employee using EmployeePub_ID

            var employeeInformation = await query.FirstOrDefaultAsync(); // get the employee model properties

            if (employeeInformation is not null)
            {
                var phoneNumberQuery = employeeInformation.PhoneNumbers.AsQueryable();

                if (!string.IsNullOrEmpty(employeeQuery.PhoneNumberPub_ID))
                    phoneNumberQuery = phoneNumberQuery.Where(e => e.PhoneNumberPub_ID == employeeQuery.PhoneNumberPub_ID);

                if (!string.IsNullOrEmpty(employeeQuery.PhoneNumberValue))
                    phoneNumberQuery = phoneNumberQuery.Where(e => e.PhoneNumberValue == employeeQuery.PhoneNumberValue);

                if (employeeQuery.Sortby.HasValue)
                    phoneNumberQuery = EmployeeSorters.ApplySortingGetPhoneNumbersAsync(phoneNumberQuery, employeeQuery.Sortby, employeeQuery.IsDecsending);

                var filteredLeaveRequest = EmployeePagination.Pagination(phoneNumberQuery,
                                                                         employeeQuery.PageNumber,
                                                                         employeeQuery.PageSize).ToList();
                employeeInformation.PhoneNumbers = filteredLeaveRequest;
            }
            return employeeInformation;
        }

        public async Task<Employee?> GetProjectAssignmentsAsync(QueryGetProjectAssignmentsAsync employeeQuery)
        {
            var query = _context.Employees.Include(e => e.ProjectAssignments).ThenInclude(e => e.Project).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(employeeQuery.EmployeePub_ID))
                query = query.Where(e => e.EmployeePub_ID == employeeQuery.EmployeePub_ID); // filter employee using EmployeePub_ID

            var employeeInformation = await query.FirstOrDefaultAsync(); // get the employee model properties

            if (employeeInformation is not null)
            {
                var projectAssignmentRequestQuery = employeeInformation.ProjectAssignments.AsQueryable();

                if (!string.IsNullOrEmpty(employeeQuery.AssignmentPub_ID))
                    projectAssignmentRequestQuery = projectAssignmentRequestQuery.Where(e =>
                                                    e.AssignmentPub_ID == employeeQuery.AssignmentPub_ID);

                if (!string.IsNullOrEmpty(employeeQuery.RoleInProject))
                    projectAssignmentRequestQuery = projectAssignmentRequestQuery.Where(e =>
                                                    e.RoleInProject == employeeQuery.RoleInProject);

                if (employeeQuery.Sortby.HasValue)
                    projectAssignmentRequestQuery = EmployeeSorters.ApplySortingGetProjectAssignmentsAsync(projectAssignmentRequestQuery, employeeQuery.Sortby, employeeQuery.IsDecsending);

                var filteredLeaveRequest = EmployeePagination.Pagination(projectAssignmentRequestQuery,
                                                                         employeeQuery.PageNumber,
                                                                         employeeQuery.PageSize).ToList();

                employeeInformation.ProjectAssignments = filteredLeaveRequest;
            }

            return employeeInformation;
        }

        public async Task<bool> isExistsAsync(string id)
        {
            return await _context.Employees.AnyAsync(d => d.EmployeePub_ID == id);
        }

        public async Task<Employee?> UpdateAsync(Guid id, Employee employee)
        {
            var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeUID == id);
            if (existingEmployee != null)
            {
                existingEmployee.EmployeePub_ID = employee.EmployeePub_ID;
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.MiddleName = employee.MiddleName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Email = employee.Email;
                existingEmployee.DateOfBirth = employee.DateOfBirth;
                existingEmployee.HireDate = employee.HireDate;
                existingEmployee.Address = employee.Address;
                existingEmployee.Status = employee.Status;
                existingEmployee.DepartmentUID = employee.DepartmentUID;
                existingEmployee.RoleUID = employee.RoleUID;
                _context.Employees.Update(existingEmployee);
                await _context.SaveChangesAsync();
                return existingEmployee;
            }
            return null; // Employee not found
        }

        public async Task<Employee?> GetByGuidAsync(Guid id)
        {
            return await _context.Employees.Include(e => e.Department)
                                           .Include(e => e.Role)
                                           .AsNoTracking()
                                           .FirstOrDefaultAsync(e => e.EmployeeUID == id);
        }

        public async Task<Employee?> GetByIdAsync(string id)
        {
            return await _context.Employees.Include(e => e.Department)
                                           .Include(e => e.Role)
                                           .AsNoTracking()
                                           .FirstOrDefaultAsync(e => e.EmployeePub_ID == id);
        }
    }
}
