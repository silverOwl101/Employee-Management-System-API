using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Queries.PhoneNumber;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System_API.Controllers
{
    [Route("api/Phone-number")]
    [ApiController]
    public class PhoneNumberController : ControllerBase
    {
        private readonly IPhoneNumberService _phoneService;
        public PhoneNumberController(IPhoneNumberService phoneService)
        {
            _phoneService = phoneService;
        }

        [HttpGet]
        [Authorize(Policy = "PhoneNumber.View")]
        public async Task<IActionResult> GetAll([FromQuery] QueryGetAllPhoneNumbers query)
        {
            var phoneNumbers = await _phoneService.GetPhoneNumbersAsync(query);
            if (phoneNumbers != null)
                return Ok(phoneNumbers);

            return NotFound("No records found.");
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "PhoneNumber.ById")]
        public async Task<IActionResult> GetbyId([FromRoute] string id)
        {
            var phoneNumber = await _phoneService.GetPhoneNumberByIdAsync(id);
            if (phoneNumber != null)
                return Ok(phoneNumber);

            return NotFound("Phone number not found!");
        }

        [HttpPost]
        [Authorize(Policy = "PhoneNumber.Create")]
        public async Task<IActionResult> Create([FromBody] UpsertPhoneNumberRequest phoneNumber)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _phoneService.AddPhoneNumberAsync(phoneNumber);
            return CreatedAtAction(nameof(GetbyId), new { id = result.PhoneNumberPub_ID }, result);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Policy = "PhoneNumber.Update")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpsertPhoneNumberRequest phoneNumber)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _phoneService.UpdatePhoneNumberAsync(id, phoneNumber);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = "PhoneNumber.Delete")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var phoneNumber = await _phoneService.DeletePhoneNumberAsync(id);
            if (phoneNumber)
                return Ok("Phone number deleted successfully.");

            return NotFound("No records found!");
        }
    }
}
