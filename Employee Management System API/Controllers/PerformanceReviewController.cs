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

        /// <summary>
        /// Get all performance review records.
        /// </summary>
        /// <param name="query">
        /// Query parameters for filtering or paginating all performance review records.
        /// </param>
        [HttpGet]
        [Authorize(Policy = "PerformanceReview.View")]
        public async Task<IActionResult> GetAll([FromQuery] QueryGetAllPerformanceReview query)
        {
            var performanceReviews = await _performanceReviewService.GetAllPerformanceReviewAsync(query);
            if (performanceReviews is not null)
                return Ok(performanceReviews);
            return NotFound("No records found.");
        }

        /// <summary>
        /// Get performance review record using performance review public id.
        /// </summary>
        /// <param name="id">
        /// Use the performance review public id.
        /// </param>        
        [HttpGet("{id}")]
        [Authorize(Policy = "PerformanceReview.ById")]
        public async Task<IActionResult> GetbyId([FromRoute] string id)
        {
            var performanceReview = await _performanceReviewService.GetPerformanceReviewByIdAsync(id);
            if (performanceReview != null)
                return Ok(performanceReview);
            return NotFound("No records found!");
        }

        /// <summary>
        /// Create new performance review record.
        /// </summary>
        /// <param name="performanceReview">
        /// Parameters for creating new performance review record.
        /// </param>        
        [HttpPost]
        [Authorize(Policy = "PerformanceReview.Create")]
        public async Task<IActionResult> Create([FromBody] UpsertPerformanceReviewRequest performanceReview)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _performanceReviewService.CreatePerformanceReviewAsync(performanceReview);
            return CreatedAtAction(nameof(GetbyId), new { id = result.ReviewPub_ID }, result);
        }

        /// <summary>
        /// Update a performance review record.
        /// </summary>
        /// <param name="id">
        /// Use the performance review public id.
        /// </param>
        /// <param name="performanceReview">
        /// Parameters for updating performance review record.
        /// </param>        
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

        /// <summary>
        /// Delete a performance review record.
        /// </summary>
        /// <param name="id">
        /// Use the performance review public id.
        /// </param>        
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
