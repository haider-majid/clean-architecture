using clean_architecture.Commands.CreateProductCommand;
using clean_architecture.Commands.DeleteProductCommand;
using clean_architecture.Commands.UpdateProductCommand;
using clean_architecture.Helpers;
using clean_architecture.Queries.GetAllProductsQuery;
using clean_architecture.Queries.GetProductByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



//[Authorize]
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
 
    public async Task<IActionResult> GetProducts([FromQuery] int skip = 0, [FromQuery] int take = 10 , [FromQuery] string search = "")
    {
        _logger.LogInformation($"Fetching products with Skip: {skip}, Take: {take}");

        try
        {
            var products = await _mediator.Send(new GetAllProductsQuery { Skip = skip, Take = take , Search = search});

            if (products == null || !products.Any())
            {
                _logger.LogWarning("No products found after applying pagination.");
                return ActionResultHelper.HandleNotFound(_logger, "No products found in the database.");
            }

            return ActionResultHelper.HandleSuccess(_logger, $"Successfully retrieved {products.Count} products.", products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching products.");
            return ActionResultHelper.HandleError(_logger, ex, "An error occurred while fetching products.");
        }
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        try
        {
            var productId = await _mediator.Send(command);
            return ActionResultHelper.HandleSuccess(_logger, $"Product created successfully with ID: {productId}", productId);
        }
        catch (Exception ex)
        {
            return ActionResultHelper.HandleError(_logger, ex, "An error occurred while creating a product.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id)
    {
        try
        {
            var product = await _mediator.Send(new GetProductByIdQuery { id = id });

            if (product == null)
                return ActionResultHelper.HandleNotFound(_logger, $"Product with ID {id} not found.");

            return ActionResultHelper.HandleSuccess(_logger, "Successfully retrieved product.", product);
        }
        catch (Exception ex)
        {
            return ActionResultHelper.HandleError(_logger, ex, $"An error occurred while fetching product with ID {id}.");
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
                return ActionResultHelper.HandleNotFound(_logger, $"Product with ID {id} not found for update.");

            return ActionResultHelper.HandleSuccess(_logger, $"Product with ID {id} updated successfully.", result);
        }
        catch (Exception ex)
        {
            return ActionResultHelper.HandleError(_logger, ex, $"An error occurred while updating product with ID {id}.");
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
                return ActionResultHelper.HandleNotFound(_logger, $"Product with ID {id} not found for deletion.");

            return ActionResultHelper.HandleSuccess(_logger, $"Product with ID {id} deleted successfully.", result);
        }
        catch (Exception ex)
        {
            return ActionResultHelper.HandleError(_logger, ex, $"An error occurred while deleting product with ID {id}.");
        }
    }
}
