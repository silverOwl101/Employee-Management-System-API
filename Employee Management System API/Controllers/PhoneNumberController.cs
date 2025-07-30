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

        /// <summary>
        /// Get all phone number records.
        /// </summary>
        /// <param name="query">
        /// Query parameters for filtering or paginating all phone number records.
        /// </param>        
        [HttpGet]
        [Authorize(Policy = "PhoneNumber.View")]
        public async Task<IActionResult> GetAll([FromQuery] QueryGetAllPhoneNumbers query)
        {
            var phoneNumbers = await _phoneService.GetPhoneNumbersAsync(query);
            if (phoneNumbers != null)
                return Ok(phoneNumbers);

            return NotFound("No records found.");
        }

        /// <summary>
        /// Get phone number record using phone number public id.
        /// </summary>
        /// <param name="id">
        /// Use the phone number public id.
        /// </param>        
        [HttpGet("{id}")]
        [Authorize(Policy = "PhoneNumber.ById")]
        public async Task<IActionResult> GetbyId([FromRoute] string id)
        {
            var phoneNumber = await _phoneService.GetPhoneNumberByIdAsync(id);
            if (phoneNumber != null)
                return Ok(phoneNumber);

            return NotFound("Phone number not found!");
        }

        /// <summary>
        /// Create new phone number record.
        /// </summary>
        /// <param name="phoneNumber">
        /// Parameters for creating new phone number record.
        /// </param>        
        [HttpPost]
        [Authorize(Policy = "PhoneNumber.Create")]
        public async Task<IActionResult> Create([FromBody] UpsertPhoneNumberRequest phoneNumber)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _phoneService.AddPhoneNumberAsync(phoneNumber);
            return CreatedAtAction(nameof(GetbyId), new { id = result.PhoneNumberPub_ID }, result);
        }

        /// <summary>
        /// Update a phone number record.
        /// </summary>
        /// <param name="id">
        /// Use the phone number public id.
        /// </param>
        /// <param name="phoneNumber">
        /// Parameters for updating phone number record.
        /// </param>        
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

        /// <summary>
        /// Delete a phone number record.
        /// </summary>
        /// <param name="id">
        /// Use the phone number public id.
        /// </param>
        /// <returns></returns>
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
