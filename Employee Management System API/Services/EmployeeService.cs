using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Mappings;
using Employee_Management_System_API.Queries.Employee;
using System.Data;

namespace Employee_Management_System_API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IRoleRepository _roleRepo;
        public EmployeeService(IEmployeeRepository employeeRepo,
                               IDepartmentRepository departmentRepo,
                               IRoleRepository roleRepo)
        {
            _employeeRepo = employeeRepo;
            _departmentRepo = departmentRepo;
            _roleRepo = roleRepo;
        }

        public async Task<EmployeeResponse> CreateEmployeeAsync(UpsertEmployeeRequest employee)
        {
            var dept = await _departmentRepo.GetByIdAsync(employee.DepartmentPub_ID);
            var role = await _roleRepo.GetByIdAsync(employee.RolePub_ID);

            var emp = new Employee
            {
                EmployeePub_ID = employee.EmployeePub_ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Email = employee.Email,
                DateOfBirth = employee.DateOfBirth,
                HireDate = employee.HireDate,
                Address = employee.Address,
                DepartmentUID = dept!.DepartmentUID,
                RoleUID = role!.RoleUID,
                AppUserId = employee.AppUserId
            };

            await _employeeRepo.CreateAsync(emp);

            return new EmployeeResponse
            {
                EmployeePub_ID = emp.EmployeePub_ID,
                FirstName = emp.FirstName,
                MiddleName = emp.MiddleName,
                LastName = emp.LastName,
                Email = emp.Email,
                DateOfBirth = emp.DateOfBirth,
                HireDate = emp.HireDate,
                Address = emp.Address,
                DepartmentPub_ID = dept!.DepartmentPub_ID,
                RolePub_ID = role!.RolePub_ID
            };
        }

        public async Task<bool> DeleteEmployeeAsync(string id)
        {
            return await _employeeRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<EmployeeResponse>> GetAllEmployeeAsync(QueryGetAllEmployees employees)
        {
            var emp = await _employeeRepo.GetAllAsync(employees);

            return emp.Select(e => new EmployeeResponse
            {
                EmployeePub_ID = e.EmployeePub_ID,
                FirstName = e.FirstName,
                MiddleName = e.MiddleName,
                LastName = e.LastName,
                Email = e.Email,
                DateOfBirth = e.DateOfBirth,
                HireDate = e.HireDate,
                Address = e.Address,
                Status = e.Status,
                DepartmentPub_ID = e.Department.DepartmentPub_ID,
                RolePub_ID = e.Role.RolePub_ID
            }).ToList();
        }

        public async Task<EmployeeAttendanceResponse> GetEmployeeAttendancesAsync(QueryGetEmployeeAttendance query)
        {
            var emp = await _employeeRepo.GetAttendancesAsync(query);

            return emp is not null ? emp.ToEmployeeAttendanceResponse() : throw new KeyNotFoundException("No records found.");
        }

        public async Task<EmployeeResponse?> GetEmployeeByGuidAsync(Guid id)
        {
            var result = await _employeeRepo.GetByGuidAsync(id);
            if (result is not null)
                return result.ToEmployeeResponse();
            return null;
        }

        public async Task<EmployeeResponse?> GetEmployeeByIdAsync(string id)
        {
            var emp = await _employeeRepo.GetByIdAsync(id);
            return emp != null ? emp.ToEmployeeResponse() : null;
        }

        public async Task<EmployeeLeaveResponse> GetEmployeeLeaveRequestsAsync(QueryGetEmployeeLeaveRequest employeeLeaveRequest)
        {
            var emp = await _employeeRepo.GetLeaveRequestsAsync(employeeLeaveRequest);

            return emp is not null ? emp.ToEmployeeLeaveResponse() : throw new KeyNotFoundException("No records found.");
        }

        public async Task<EmployeePayrollResponse> GetEmployeePayrollsAsync(QueryGetEmployeePayroll employeePayrollRequest)
        {
            var emp = await _employeeRepo.GetPayrollsAsync(employeePayrollRequest);

            return emp is not null ? emp.ToEmployeePayrollResponse() : throw new KeyNotFoundException("No records found.");
        }

        public async Task<EmployeePerformanceReviewResponse> GetEmployeePerformanceReviewsAsync(QueryGetPerformanceReviewsAsync employeeQuery)
        {
            var emp = await _employeeRepo.GetPerformanceReviewsAsync(employeeQuery);

            return emp is not null ? emp.ToEmployeePerformanceReviewResponse() : throw new KeyNotFoundException("No records found.");
        }

        public async Task<EmployeePhoneNumberResponse> GetEmployeePhoneNumbersAsync(QueryGetPhoneNumbersAsync employeeQuery)
        {
            var emp = await _employeeRepo.GetPhoneNumbersAsync(employeeQuery);

            return emp is not null ? emp.ToPhoneNumbersResponse() : throw new KeyNotFoundException("No records found.");
        }

        public async Task<EmployeeProjectAssignmentResponse> GetEmployeeProjectAssignmentsAsync(QueryGetProjectAssignmentsAsync employeeQuery)
        {
            var emp = await _employeeRepo.GetProjectAssignmentsAsync(employeeQuery);

            return emp is not null ? emp.ToEmployeeProjectAssignmentResponse() : throw new KeyNotFoundException("No records found.");
        }

        public async Task<bool> isEmployeeExistsAsync(string id)
        {
            return await _employeeRepo.isExistsAsync(id);
        }

        public async Task<EmployeeResponse?> UpdateEmployeeAsync(string id, UpsertEmployeeRequest employee)
        {
            var existingEmployee = await _employeeRepo.GetByIdAsync(id);
            if (existingEmployee != null)
            {
                var dept = await _departmentRepo.GetByIdAsync(employee.DepartmentPub_ID);
                var role = await _roleRepo.GetByIdAsync(employee.RolePub_ID);

                if (dept == null)
                {
                    throw new KeyNotFoundException($"Department with ID {employee.DepartmentPub_ID} not found.");
                }
                if (role == null)
                {
                    throw new KeyNotFoundException($"Role with ID {employee.RolePub_ID} not found.");
                }

                var newEmployeeValues = new Employee
                {
                    FirstName = employee.FirstName,
                    MiddleName = employee.MiddleName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    HireDate = employee.HireDate,
                    Address = employee.Address,
                    DepartmentUID = dept.DepartmentUID,
                    RoleUID = role.RoleUID
                };

                var updatedEmployee = await _employeeRepo.UpdateAsync(existingEmployee.EmployeeUID, newEmployeeValues);

                return updatedEmployee != null ? new EmployeeResponse
                {
                    EmployeePub_ID = newEmployeeValues.EmployeePub_ID,
                    FirstName = newEmployeeValues.FirstName,
                    MiddleName = newEmployeeValues.MiddleName,
                    LastName = newEmployeeValues.LastName,
                    Email = newEmployeeValues.Email,
                    DateOfBirth = newEmployeeValues.DateOfBirth,
                    HireDate = newEmployeeValues.HireDate,
                    Address = newEmployeeValues.Address,
                    DepartmentPub_ID = dept!.DepartmentPub_ID,
                    RolePub_ID = role!.RolePub_ID
                } : throw new KeyNotFoundException("Error updating the employee.");
            }
            throw new KeyNotFoundException("Employee not found!");
        }
    }
}
