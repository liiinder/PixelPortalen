using Microsoft.AspNetCore.Mvc;
using PixelPortalen.API.Services;
using PixelPortalen.Shared.DTO;

[Route("api/[controller]")]
[ApiController]
public class CustomerRatingsController : ControllerBase
{
    private readonly CustomerRatingService _service;

    public CustomerRatingsController(CustomerRatingService service)
    {
        _service = service;
    }

    [HttpPost("{customerId}")]
    public async Task<IActionResult> PostRating(int customerId, [FromBody] CustomerRatingDTO dto)
    {
        if (dto.Rating < 1 || dto.Rating > 5)
            return BadRequest("Betyget måste vara mellan 1 och 5.");

        var rating = await _service.AddRatingAsync(customerId, dto);
        return Ok(new { message = "Betyg inlämnat.", rating });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerRating>>> GetAll()
    {
        var ratings = await _service.GetAllAsync();
        return Ok(ratings);
    }

    [HttpGet("customer/{id}")]
    public async Task<ActionResult<IEnumerable<CustomerRating>>> GetAllByCustomerId(int id)
    {
        var ratings = await _service.GetByCustomerIdAsync(id);
        return Ok(ratings);
    }

    [HttpGet("product/{productId}")]
    public async Task<ActionResult<IEnumerable<CustomerRating>>> GetByProduct(int productId)
    {
        var ratings = await _service.GetRatingsByProductAsync(productId);
        return Ok(ratings);
    }

}
