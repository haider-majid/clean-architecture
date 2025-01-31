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

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var query = new GetAllProductsQuery { };
        var products = await _mediator.Send(query);
        if (query == null)
        {
            return NotFound();
            
        }
        return Ok(products);
    }
    
    [HttpGet("{id}")]

    
    public async Task<IActionResult> GetProduct([FromRoute] Guid id)
    {
        var query = new GetProductByIdQuery { id = id };
        var product = await _mediator.Send(query);

        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var productId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id = productId }, productId);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
    {
        var product = await _mediator.Send(command);
        return Ok(command);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteProductCommand command)
    {
        var product = await _mediator.Send(command);
        return Ok(command);

    }
}