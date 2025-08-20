using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Queries.LeaveRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System_API.Controllers
{
    [Route("api/Timeoff")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;
        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        /// <summary>
        /// Get all leave request records.
        /// </summary>
        /// <param name="query">
        /// Query parameters for filtering or paginating all leave request records.
        /// </param>        
        [HttpGet]
        [Authorize(Policy = "LeaveRequest.View")]
        public async Task<IActionResult> GetAll([FromQuery] QueryGetAllLeaveRequest query)
        {
            var leaveRequests = await _leaveRequestService.GetAllLeaveRequestAsync(query);
            if (leaveRequests != null)
                return Ok(leaveRequests);
            return NotFound("No records found.");
        }

        /// <summary>
        /// Get leave request record using leave request public id.
        /// </summary>
        /// <param name="id">
        /// Use the leave request public id.
        /// </param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "LeaveRequest.ById")]
        public async Task<IActionResult> GetbyId([FromRoute] string id)
        {
            var leaveRequest = await _leaveRequestService.GetLeaveRequestByIdAsync(id);
            if (leaveRequest != null)
                return Ok(leaveRequest);
            return NotFound("No records found!");
        }

        /// <summary>
        /// Create new leave request record.
        /// </summary>
        /// <param name="leaveRequest">
        /// Parameters for creating new leave request record.
        /// </param>        
        [HttpPost]
        [Authorize(Policy = "LeaveRequest.Create")]
        public async Task<IActionResult> Create([FromBody] UpsertLeaveRequest_Request leaveRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _leaveRequestService.CreateLeaveRequestAsync(leaveRequest);
            return CreatedAtAction(nameof(GetbyId), new { id = result.LeavePub_ID }, result);
        }

        /// <summary>
        /// Update a leave request record.
        /// </summary>
        /// <param name="id">
        /// Use the leave request public id.
        /// </param>
        /// <param name="leaveRequest">
        /// Parameters for updating a leave request record.
        /// </param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [Authorize(Policy = "LeaveRequest.Update")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpsertLeaveRequest_Request leaveRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _leaveRequestService.UpdateLeaveRequestAsync(id, leaveRequest);
            return Ok(result);
        }

        /// <summary>
        /// Delete a leave request record.
        /// </summary>
        /// <param name="id">
        /// Use the leave request public id.
        /// </param>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = "LeaveRequest.Delete")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var leaveRequest = await _leaveRequestService.DeleteLeaveRequestAsync(id);
            if (leaveRequest)
                return Ok("Leave request deleted successfully.");
            return NotFound("No records found!");
        }
    }
}
