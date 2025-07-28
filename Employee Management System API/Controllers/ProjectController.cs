using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Queries.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System_API.Controllers
{
    [Route("api/Project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [Authorize(Policy = "Project.View")]
        public async Task<IActionResult> GetAll([FromQuery] QueryGetAllProject query)
        {
            var projects = await _projectService.GetAllProjectAsync(query);
            if (projects is not null && projects.Any())
                return Ok(projects);
            return NotFound("No records found.");
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Project.ById")]
        public async Task<IActionResult> GetbyId([FromRoute] string id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project != null)
                return Ok(project);
            return NotFound("No records found!");
        }

        [HttpGet("{id}/employees-assigned")]
        [Authorize(Policy = "Project.GetEmployees")]
        public async Task<IActionResult> GetAssignedEmployees([FromRoute] string id, [FromQuery] QueryGetById query)
        {
            var project = await _projectService.GetAssignedEmployeesOfTheProject(id, query);
            if (project is not null)
                return Ok(project);
            return NotFound("No records found!");
        }

        [HttpPost]
        [Authorize(Policy = "Project.Create")]
        public async Task<IActionResult> Create([FromBody] UpsertProjectRequest project)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _projectService.CreateProjectAsync(project);
            return CreatedAtAction(nameof(GetbyId), new { id = result.ProjectPub_ID }, result);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Policy = "Project.Update")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpsertProjectRequest project)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _projectService.UpdateProjectAsync(id, project);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = "Project.Delete")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var project = await _projectService.DeleteProjectAsync(id);
            if (project)
                return Ok("Project deleted successfully.");
            return NotFound("No records found!");
        }
    }
}
