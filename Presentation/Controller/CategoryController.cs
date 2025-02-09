using clean_architecture.Application.Features.Category.Command.AddCategoryCommand;
using clean_architecture.Application.Features.Category.Command.RemoveGategoryCommand;
using clean_architecture.Application.Features.Category.Command.UpdateCategoryCommand;
using clean_architecture.Application.Features.Category.Query.GetCategoryQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace clean_architecture.Controllers;


[ApiController]
[Route("api/v1/categories")]

public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var query = new GetAllCategoryQuery() { };
        var categories = await _mediator.Send(query);
        if (query == null)
        {
            return NotFound();
            
        }
        return Ok(categories);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory([FromRoute] Guid id)
    {
        var query = new GetCategoryQuery { Id = id };
        var category = await _mediator.Send(query);

        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveCategory([FromRoute] Guid id)
    {
        var command = new RemoveGategoryCommand { id = id };
        var result = await _mediator.Send(command);
        if (result ==false)
        {
            return NotFound("The category does not exist."); // Send a 404 response
        }
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCategory), new { id = result.id }, result);
    }
  
    
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryCommand command)
    {
       

        // Assign the route ID to the command
        command.Id = id;
        

        var result = await _mediator.Send(command);
        if (result == null)
        {
            return NotFound("The category does not exist."); // Send a 404 response
        }
    
        return Ok(result);
    }

    
}