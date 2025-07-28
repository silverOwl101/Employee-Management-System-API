using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Mappings;
using Employee_Management_System_API.Queries.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System_API.Controllers
{
    [Route("api/Role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Authorize(Policy = "Role.View")]
        public async Task<IActionResult> GetAll([FromQuery] QueryGetAllRole query)
        {
            var listOfRoles = await _roleService.GetAllRolesAsync(query);
            if(listOfRoles is not null)
                return Ok(listOfRoles);
            return NotFound("No departments found.");            
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Role.ById")]
        public async Task<IActionResult> GetbyId([FromRoute] string id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role != null)
                return Ok(role);
            return NotFound($"Role not found");
        }

        [HttpPost]
        [Authorize(Policy = "Role.Create")]
        public async Task<IActionResult> Create([FromBody] UpsetRoleRequest role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.CreateRoleAsync(role);
            return CreatedAtAction(nameof(GetbyId), new { id = result.RolePub_ID }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Role.Update")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpsetRoleRequest role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.UpdateRoleAsync(id, role);
            if(result is not null)
                return Ok(result);
            return BadRequest("Update cannot be completed!");
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Role.Delete")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.DeleteRoleAsync(id);
            if(result)
                return Ok("Role deleted successfully.");
            return NotFound("Role not found!");
        }
    }
}

