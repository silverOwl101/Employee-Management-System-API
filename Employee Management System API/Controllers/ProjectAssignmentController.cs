using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Queries.ProjectAssignment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System_API.Controllers
{
    [Route("api/Assignments")]
    [ApiController]
    public class ProjectAssignmentController : ControllerBase
    {
        private readonly IProjectAssignmentService _projectAssignmentService;
        public ProjectAssignmentController(IProjectAssignmentService projectAssignmentService)
        {
            _projectAssignmentService = projectAssignmentService;
        }

        [HttpGet]
        [Authorize(Policy = "ProjectAssignment.View")]
        public async Task<IActionResult> GetAll([FromQuery] QueryGetAllProjectAssignment query)
        {
            var projectAssigments = await _projectAssignmentService.GetAllProjectAssignment(query);
            if (projectAssigments is not null)
                return Ok(projectAssigments);
            return NotFound("No records found.");
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ProjectAssignment.ById")]
        public async Task<IActionResult> GetbyId([FromRoute] string id)
        {
            var projectAssigment = await _projectAssignmentService.GetProjectAssignmentById(id);
            if (projectAssigment != null)
                return Ok(projectAssigment);
            return NotFound("No records found!");
        }

        [HttpPost]
        [Authorize(Policy = "ProjectAssignment.Create")]
        public async Task<IActionResult> Create([FromBody] UpsertProjectAssignmentRequest projectAssignment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _projectAssignmentService.CreateProjectAssignment(projectAssignment);
            return CreatedAtAction(nameof(GetbyId), new { id = result.AssignmentPub_ID }, result);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Policy = "ProjectAssignment.Update")]
        public async Task<IActionResult> Update([FromRoute] string id,
                                                [FromBody] UpsertProjectAssignmentRequest projectAssignment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _projectAssignmentService.UpdateProjectAssignment(id, projectAssignment);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = "ProjectAssignment.Delete")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var projectAssignment = await _projectAssignmentService.DeleteProjectAssignment(id);
            if (projectAssignment)
                return Ok("Assignment deleted successfully.");
            return NotFound("No records found!");
        }
    }
}
