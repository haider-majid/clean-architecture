using clean_architecture.Commands.CreateProductCommand;
using clean_architecture.Commands.DeleteProductCommand;
using clean_architecture.Commands.UpdateProductCommand;
using clean_architecture.Queries.GetAllProductsQuery;
using clean_architecture.Queries.GetProductByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace clean_architecture.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            var products = await _mediator.Send(new GetAllProductsQuery());

            if (products == null || !products.Any())
            {
                _logger.LogWarning("No products found in the database.");
                return NotFound("No products available.");
            }

            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching products.");
            return StatusCode(500, "An internal server error occurred.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        try
        {
            var productId = await _mediator.Send(command);
            _logger.LogInformation("Product created successfully with ID: {ProductId}", productId);
            
            return CreatedAtAction(nameof(GetProduct), new { id = productId }, productId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a product.");
            return StatusCode(500, "An internal server error occurred.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id)
    {
        try
        {
            var product = await _mediator.Send(new GetProductByIdQuery { id = id });

            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found.", id);
                return NotFound("Product not found.");
            }

            return Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching product with ID {ProductId}.", id);
            return StatusCode(500, "An internal server error occurred.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductCommand command)
    {
        try
        {
            command.id = id;
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for update.", id);
                return NotFound("Product not found.");
            }

            _logger.LogInformation("Product with ID {ProductId} updated successfully.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating product with ID {ProductId}.", id);
            return StatusCode(500, "An internal server error occurred.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            var command = new DeleteProductCommand { id = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for deletion.", id);
                return NotFound("Product not found.");
            }

            _logger.LogInformation("Product with ID {ProductId} deleted successfully.", id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting product with ID {ProductId}.", id);
            return StatusCode(500, "An internal server error occurred.");
        }
    }
}
