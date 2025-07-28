using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Queries.PerformanceReview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System_API.Controllers
{
    [Route("api/Review")]
    [ApiController]
    public class PerformanceReviewController : ControllerBase
    {
        private readonly IPerformanceReviewService _performanceReviewService;
        public PerformanceReviewController(IPerformanceReviewService performanceReviewService)
        {
            _performanceReviewService = performanceReviewService;
        }

        [HttpGet]
        [Authorize(Policy = "PerformanceReview.View")]
        public async Task<IActionResult> GetAll([FromQuery] QueryGetAllPerformanceReview query)
        {
            var performanceReviews = await _performanceReviewService.GetAllPerformanceReviewAsync(query);
            if (performanceReviews is not null)
                return Ok(performanceReviews);
            return NotFound("No records found.");
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "PerformanceReview.ById")]
        public async Task<IActionResult> GetbyId([FromRoute] string id)
        {
            var performanceReview = await _performanceReviewService.GetPerformanceReviewByIdAsync(id);
            if (performanceReview != null)
                return Ok(performanceReview);
            return NotFound("No records found!");
        }

        [HttpPost]
        [Authorize(Policy = "PerformanceReview.Create")]
        public async Task<IActionResult> Create([FromBody] UpsertPerformanceReviewRequest performanceReview)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _performanceReviewService.CreatePerformanceReviewAsync(performanceReview);
            return CreatedAtAction(nameof(GetbyId), new { id = result.ReviewPub_ID }, result);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Policy = "PerformanceReview.Update")]
        public async Task<IActionResult> Update([FromRoute] string id,
                                                [FromBody] UpsertPerformanceReviewRequest performanceReview)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _performanceReviewService.UpdatePerformanceReviewAsync(id, performanceReview);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = "PerformanceReview.Delete")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var performanceReview = await _performanceReviewService.DeletePerformanceReviewAsync(id);
            if (performanceReview)
                return Ok("Performance review deleted successfully.");
            return NotFound("No records found!");
        }
    }
}
