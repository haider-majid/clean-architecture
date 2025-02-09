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
        var query = new GetCategoryQuery() { };
        var categories = await _mediator.Send(query);
        if (query == null)
        {
            return NotFound();
            
        }
        return Ok(categories);
    }
    


    
  
    
  
    
}