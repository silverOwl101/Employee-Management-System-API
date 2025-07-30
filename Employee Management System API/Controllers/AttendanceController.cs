using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Queries.Attendance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System_API.Controllers
{
    [Route("api/Attendance")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        /// <summary>
        /// Get all attendance records
        /// </summary>
        /// <param name="query">
        /// Query parameters for filtering or paginating the all employee’s attendance.
        /// </param>        
        [HttpGet]
        [Authorize(Policy = "Attendance.View")]
        public async Task<IActionResult> GetAll([FromQuery] QueryGetAllAttendance query)
        {
            var attendance = await _attendanceService.GetAllAttendanceAsync(query);
            if (attendance != null)
                return Ok(attendance);            
            return NotFound("No records found.");
        }

        /// <summary>
        /// Get attendance record using attendance public id
        /// </summary>
        /// <param name="id">
        /// Use the attendance public id
        /// </param>        
        [HttpGet("{id}")]
        [Authorize(Policy = "Attendance.ById")]
        public async Task<IActionResult> GetbyId([FromRoute] string id)
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
            if (attendance != null)
                return Ok(attendance);
            return NotFound("No records found!");
        }

        /// <summary>
        /// Create a new attendance record
        /// </summary>        
        [HttpPost]
        [Authorize(Policy = "Attendance.Create")]
        public async Task<IActionResult> Create([FromBody] UpsertAttendanceRequest attendance)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _attendanceService.CreateAttendanceAsync(attendance);
            return CreatedAtAction(nameof(GetbyId), new { id = result.AttendancePub_ID }, result);
        }


        /// <summary>
        /// Update an attendance record
        /// </summary>
        /// <param name="id">
        /// Use the attendance public id
        /// </param>
        /// <param name="attendance">
        /// Parameters for updating employee's attendance.
        /// </param>        
        [HttpPut]
        [Route("{id}")]
        [Authorize(Policy = "Attendance.Update")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpsertAttendanceRequest attendance)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _attendanceService.UpdateAttendanceAsync(id, attendance);
            return Ok(result);
        }


        /// <summary>
        /// Delete an attendance record
        /// </summary>
        /// <param name="id">
        /// Use the attendance public id
        /// </param>        
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = "Attendance.Delete")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var attendance = await _attendanceService.DeleteAttendanceAsync(id);
            if (attendance)
                return Ok("Attendance deleted successfully.");
            return NotFound("No records found!");
        }
    }
}
