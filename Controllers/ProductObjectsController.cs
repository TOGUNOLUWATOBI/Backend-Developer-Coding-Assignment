using Microsoft.AspNetCore.Mvc;
using RestfulApiExtension.Models;
using RestfulApiExtension.Services;
using RestfulApiExtension.Services.Interfaces;

namespace RestfulApiExtension.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductObjectsController : ControllerBase
{
    private readonly IProductObjectService _service;

    public ProductObjectsController(IProductObjectService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? name, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var results = await _service.GetAllAsync(name, page, pageSize);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound("No productObject found.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductObject productObject)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.CreateAsync(productObject);
            if (result == null)
                return StatusCode(500, "Failed to create productObject.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound("Failed to delete productObject.");
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
}
