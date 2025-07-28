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
        /// View all employees.
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

        [HttpGet("{id}")]
        [Authorize(Policy = "Employee.ById")]
        public async Task<IActionResult> GetbyId([FromRoute] string id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee != null)
                return Ok(employee);

            return NotFound("Employee not found.");
        }

        [HttpGet("attendance")]
        [Authorize(Policy = "Employee.Attendance")]
        public async Task<IActionResult> GetAttendance([FromQuery] QueryGetEmployeeAttendance employeeAttendance)
        {
            var attendancRecords = await _employeeService.GetEmployeeAttendancesAsync(employeeAttendance);
            if (attendancRecords != null)
                return Ok(attendancRecords);

            return NotFound("No records found.");
        }

        [HttpGet("leave")]
        [Authorize(Policy = "Employee.LeaveRequest")]
        public async Task<IActionResult> GetLeaveRequest([FromQuery] QueryGetEmployeeLeaveRequest employeeLeaveRequest)
        {
            var employee = await _employeeService.GetEmployeeLeaveRequestsAsync(employeeLeaveRequest);
            if (employee != null)
                return Ok(employee);

            return NotFound("No records found.");
        }

        [HttpGet("payroll")]
        [Authorize(Policy = "Employee.Payroll")]
        public async Task<IActionResult> GetPayroll([FromQuery] QueryGetEmployeePayroll employeePayrollRequest)
        {
            var employee = await _employeeService.GetEmployeePayrollsAsync(employeePayrollRequest);
            if (employee != null)
                return Ok(employee);

            return NotFound("No records found.");
        }

        [HttpGet("appraisal")]
        [Authorize(Policy = "Employee.PerformanceReview")]
        public async Task<IActionResult> GetPerformanceReview([FromQuery] QueryGetPerformanceReviewsAsync employeePerformanceReviewRequest)
        {
            var employee = await _employeeService.GetEmployeePerformanceReviewsAsync(employeePerformanceReviewRequest);
            if (employee != null)
                return Ok(employee);

            return NotFound("No records found.");
        }

        [HttpGet("contact")]
        [Authorize(Policy = "Employee.PhoneNumbers")]
        public async Task<IActionResult> GetPhoneNumbers([FromQuery] QueryGetPhoneNumbersAsync employeePhoneNumberRequest)
        {
            var employee = await _employeeService.GetEmployeePhoneNumbersAsync(employeePhoneNumberRequest);
            if (employee != null)
                return Ok(employee);

            return NotFound("No records found.");
        }

        [HttpGet("task")]
        [Authorize(Policy = "Employee.ProjectAssignment")]
        public async Task<IActionResult> GetProjectAssignment([FromQuery] QueryGetProjectAssignmentsAsync employeeProjectAssigmentRequest)
        {
            var employee = await _employeeService.GetEmployeeProjectAssignmentsAsync(employeeProjectAssigmentRequest);
            if (employee != null)
                return Ok(employee);

            return NotFound("No records found.");
        }

        [HttpPost]
        [Authorize(Policy = "Employee.Create")]
        public async Task<IActionResult> Create([FromBody] UpsertEmployeeRequest emp)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.CreateEmployeeAsync(emp);
            return CreatedAtAction(nameof(GetbyId), new { id = result.EmployeePub_ID }, result);
        }

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
