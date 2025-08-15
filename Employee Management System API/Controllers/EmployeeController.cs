using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Queries.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System_API.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get all employee records 
        /// </summary>                
        [HttpGet]
        [Authorize(Policy = "Employee.View")]
        public async Task<IActionResult> GetAll([FromQuery] QueryGetAllEmployees employees)
        {
            var listOfEmployees = await _employeeService.GetAllEmployeeAsync(employees);
            if (listOfEmployees != null)
                return Ok(listOfEmployees);

            return NotFound("No records found.");
        }

        /// <summary>
        /// Get the employee record details using employee public id
        /// </summary>
        /// <param name="id">
        /// Use the employee public id
        /// </param>        
        [HttpGet("{id}")]
        [Authorize(Policy = "Employee.ById")]
        public async Task<IActionResult> GetbyId([FromRoute] string id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee != null)
                return Ok(employee);

            return NotFound("Employee not found.");
        }

        /// <summary>
        /// Get the attendance of the employee
        /// </summary>
        /// <param name="id">
        /// Use the employee public id
        /// </param>
        /// <param name="employeeAttendance">
        /// Query parameters for filtering or paginating the employee’s attendance.
        /// </param>        
        [HttpGet("attendance")]
        [Authorize(Policy = "Employee.Attendance")]
        public async Task<IActionResult> GetAttendance([FromRoute] string id,
                                                       [FromQuery] QueryGetEmployeeAttendance
                                                                   employeeAttendance)
        {
            var attendancRecords = await _employeeService.GetEmployeeAttendancesAsync(id, employeeAttendance);
            if (attendancRecords != null)
                return Ok(attendancRecords);

            return NotFound("No records found.");
        }

        /// <summary>
        /// Get the leave request of the employee
        /// </summary>
        /// <param name="id">
        /// Use the employee public id
        /// </param>
        /// <param name="employeeLeaveRequest">
        /// Query parameters for filtering or paginating the employee’s leave request.
        /// </param>        
        [HttpGet("leave")]
        [Authorize(Policy = "Employee.LeaveRequest")]
        public async Task<IActionResult> GetLeaveRequest([FromRoute] string id,
                                                         [FromQuery] QueryGetEmployeeLeaveRequest
                                                                     employeeLeaveRequest)
        {
            var employee = await _employeeService.GetEmployeeLeaveRequestsAsync(id, employeeLeaveRequest);
            if (employee != null)
                return Ok(employee);

            return NotFound("No records found.");
        }

        /// <summary>
        /// Get the payroll of the employee
        /// </summary>
        /// <param name="id">
        /// Use the employee public id
        /// </param>
        /// <param name="employeePayrollRequest">
        /// Query parameters for filtering or paginating the employee’s payroll request.
        /// </param>        
        [HttpGet("payroll")]
        [Authorize(Policy = "Employee.Payroll")]
        public async Task<IActionResult> GetPayroll([FromRoute] string id,
                                                    [FromQuery] QueryGetEmployeePayroll
                                                                employeePayrollRequest)
        {
            var employee = await _employeeService.GetEmployeePayrollsAsync(id, employeePayrollRequest);
            if (employee != null)
                return Ok(employee);

            return NotFound("No records found.");
        }

        /// <summary>
        /// Get the performance review of the employee
        /// </summary>
        /// <param name="id">
        /// Use the employee public id
        /// </param>
        /// <param name="employeePerformanceReviewRequest">
        /// Query parameters for filtering or paginating the employee’s performance review.
        /// </param>        
        [HttpGet("appraisal")]
        [Authorize(Policy = "Employee.PerformanceReview")]
        public async Task<IActionResult> GetPerformanceReview([FromRoute] string id,
                                                              [FromQuery] QueryGetPerformanceReviewsAsync
                                                                          employeePerformanceReviewRequest)
        {
            var employee = await _employeeService.GetEmployeePerformanceReviewsAsync(id, employeePerformanceReviewRequest);
            if (employee != null)
                return Ok(employee);

            return NotFound("No records found.");
        }

        /// <summary>
        /// Get the contact number of the employee
        /// </summary>
        /// <param name="id">
        /// Use the employee public id
        /// </param>
        /// <param name="employeePhoneNumberRequest">
        /// Query parameters for filtering or paginating the employee’s phone number.
        /// </param>        
        [HttpGet("contact")]
        [Authorize(Policy = "Employee.PhoneNumbers")]
        public async Task<IActionResult> GetPhoneNumbers([FromRoute] string id,
                                                         [FromQuery] QueryGetPhoneNumbersAsync
                                                                     employeePhoneNumberRequest)
        {
            var employee = await _employeeService.GetEmployeePhoneNumbersAsync(id, employeePhoneNumberRequest);
            if (employee != null)
                return Ok(employee);

            return NotFound("No records found.");
        }

        /// <summary>
        /// Get the project assignment of the employee
        /// </summary>
        /// <param name="id">
        /// Use the employee public id
        /// </param>
        /// <param name="employeeProjectAssigmentRequest">
        /// Query parameters for filtering or paginating the employee’s project assignments.
        /// </param>        
        [HttpGet("task")]
        [Authorize(Policy = "Employee.ProjectAssignment")]
        public async Task<IActionResult> GetProjectAssignment([FromRoute] string id,
                                                              [FromQuery] QueryGetProjectAssignmentsAsync
                                                                          employeeProjectAssigmentRequest)
        {
            var employee = await _employeeService.GetEmployeeProjectAssignmentsAsync(id, employeeProjectAssigmentRequest);
            if (employee != null)
                return Ok(employee);

            return NotFound("No records found.");
        }

        /// <summary>
        /// Create new employee record
        /// </summary>        
        [HttpPost]
        [Authorize(Policy = "Employee.Create")]
        public async Task<IActionResult> Create([FromBody] UpsertEmployeeRequest emp)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.CreateEmployeeAsync(emp);
            return CreatedAtAction(nameof(GetbyId), new { id = result.EmployeePub_ID }, result);
        }

        /// <summary>
        /// Update an employee record
        /// </summary>
        /// <param name="id">
        /// Use the employee public id
        /// </param>
        /// /// <param name="emp">
        /// Parameters updating the employee’s information.
        /// </param>        
        [HttpPut]
        [Route("{id}")]
        [Authorize(Policy = "Employee.Update")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpsertEmployeeRequest emp)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.UpdateEmployeeAsync(id, emp);
            return Ok(result);
        }

        /// <summary>
        /// Delete an employee record
        /// </summary>        
        /// <param name="id">
        /// Use the employee public id
        /// </param>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = "Employee.Delete")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = await _employeeService.DeleteEmployeeAsync(id);
            return Ok("Employee deleted successfully.");
        }
    }
}
