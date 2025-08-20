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

        /// <summary>
        /// Get all employee job role records.
        /// </summary>
        /// <param name="query">
        /// Query parameters for filtering or paginating all employee job role records.
        /// </param>        
        [HttpGet]
        [Authorize(Policy = "Role.View")]
        public async Task<IActionResult> GetAll([FromQuery] QueryGetAllRole query)
        {
            var listOfRoles = await _roleService.GetAllRolesAsync(query);
            if(listOfRoles is not null)
                return Ok(listOfRoles);
            return NotFound("No records found.");            
        }

        /// <summary>
        /// Get employee job role record using role public id.
        /// </summary>
        /// <param name="id">
        /// Use the role public id.
        /// </param>        
        [HttpGet("{id}")]
        [Authorize(Policy = "Role.ById")]
        public async Task<IActionResult> GetbyId([FromRoute] string id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role != null)
                return Ok(role);
            return NotFound($"Role not found");
        }

        /// <summary>
        /// Create new employee job role record.
        /// </summary>
        /// <param name="role">
        /// Parameters for creating new employee job role record.
        /// </param>        
        [HttpPost]
        [Authorize(Policy = "Role.Create")]
        public async Task<IActionResult> Create([FromBody] UpsetRoleRequest role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.CreateRoleAsync(role);
            return CreatedAtAction(nameof(GetbyId), new { id = result.RolePub_ID }, result);
        }

        /// <summary>
        /// Update role record.
        /// </summary>
        /// <param name="id">
        /// Use the role public id.
        /// </param>
        /// <param name="role">
        /// Parameters for updating employee job role record.
        /// </param>        
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

        /// <summary>
        /// Delete a role record.
        /// </summary>
        /// <param name="id">
        /// Use the role public id.
        /// </param>        
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

