using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Queries.Payroll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System_API.Controllers
{
    [Route("api/Payroll")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;
        public PayrollController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        /// <summary>
        /// Get all payroll records.
        /// </summary>
        /// <param name="query">
        /// Query parameters for filtering or paginating all payroll records.
        /// </param>        
        [HttpGet]
        [Authorize(Policy = "Payroll.View")]
        public async Task<IActionResult> GetAll([FromQuery] QueryGetAllPayroll query)
        {
            var payroll = await _payrollService.GetAllPayrollAsync(query);
            if (payroll is not null)
                return Ok(payroll);
            return NotFound("No records found.");
        }

        /// <summary>
        /// Get payroll records using payroll public id.
        /// </summary>
        /// <param name="id">
        /// Use the payroll public id.
        /// </param>        
        [HttpGet("{id}")]
        [Authorize(Policy = "Payroll.ById")]
        public async Task<IActionResult> GetbyId([FromRoute] string id)
        {
            var payroll = await _payrollService.GetPayrollByIdAsync(id);
            if (payroll != null)
                return Ok(payroll);
            return NotFound("No records found!");
        }

        /// <summary>
        /// Create new payroll record.
        /// </summary>
        /// <param name="payroll">
        /// Parameters for creating new payroll record.
        /// </param>        
        [HttpPost]
        [Authorize(Policy = "Payroll.Create")]
        public async Task<IActionResult> Create([FromBody] UpsertPayrollRequest payroll)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _payrollService.CreatePayrollAsync(payroll);
            return CreatedAtAction(nameof(GetbyId), new { id = result.PayrollPub_ID }, result);
        }

        /// <summary>
        /// Update a payroll record.
        /// </summary>
        /// <param name="id">
        /// Use the payroll public id.
        /// </param>
        /// <param name="payroll">
        /// Parameters for updating payroll record.
        /// </param>        
        [HttpPut]
        [Route("{id}")]
        [Authorize(Policy = "Payroll.Update")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpsertPayrollRequest payroll)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _payrollService.UpdatePayrollAsync(id, payroll);
            return Ok(result);
        }

        /// <summary>
        /// Delete a payroll record.
        /// </summary>
        /// <param name="id">
        /// Parameters for deleting payroll record.
        /// </param>        
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = "Payroll.Delete")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var payroll = await _payrollService.DeletePayrollAsync(id);
            if (payroll)
                return Ok("Payroll deleted successfully.");
            return NotFound("No records found!");
        }
    }
}
