using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Mappings;
using Employee_Management_System_API.Queries.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Employee_Management_System_API.Controllers
{
    [Route("api/Department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        [Authorize(Policy = "Department.View")]
        public async Task<IActionResult> GetAll([FromQuery] QueryGetAllDepartment query)
        {
            var listOfDepartments = await _departmentService.GetAllDepartment(query);
            if (listOfDepartments is not null)
                return Ok(listOfDepartments);
            return NotFound("No records found.");
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Department.ById")]
        public async Task<IActionResult> GetbyId([FromRoute] string id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            if(department is not null)
                return Ok(department);
            return NotFound("Department not found!");
        }        

        [HttpPost]
        [Authorize(Policy = "Department.Create")]
        public async Task<IActionResult> Create([FromBody] UpsertDepartmentRequest dept)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.CreateDepartmentAsync(dept);
            return CreatedAtAction(nameof(GetbyId), new { id = result.DepartmentPub_ID }, result);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Policy = "Department.Update")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpsertDepartmentRequest dept)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.UpdateDepartmentAsync(id, dept);
            if(result is not null)
                return Ok(result);
            return BadRequest("Invalid parameters");            
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = "Department.Delete")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = await _departmentService.DeleteDepartmentAsync(id);
            if(department)
                return Ok("Department deleted successfully.");
            return NotFound("Department not found!");            
        }
    }
}
