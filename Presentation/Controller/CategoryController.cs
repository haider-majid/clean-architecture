using clean_architecture.Application.Features.Category.Command.AddCategoryCommand;
using clean_architecture.Application.Features.Category.Command.RemoveGategoryCommand;
using clean_architecture.Application.Features.Category.Command.UpdateCategoryCommand;
using clean_architecture.Application.Features.Category.Query.GetCategoryQuery;
using clean_architecture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace clean_architecture.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(IMediator mediator, ILogger<CategoryController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var categories = await _mediator.Send(new GetAllCategoryQuery());

            if (categories == null || !categories.Any())
                return ActionResultHelper.HandleNotFound(_logger, "No categories found.");

            return ActionResultHelper.HandleSuccess(_logger, $"Successfully retrieved {categories.Count} categories.", categories);
        }
        catch (Exception ex)
        {
            return ActionResultHelper.HandleError(_logger, ex, "An error occurred while fetching categories.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory([FromRoute] Guid id)
    {
        try
        {
            var category = await _mediator.Send(new GetCategoryQuery { Id = id });

            if (category == null)
                return ActionResultHelper.HandleNotFound(_logger, $"Category with ID {id} not found.");

            return ActionResultHelper.HandleSuccess(_logger, $"Successfully retrieved category {id}.", category);
        }
        catch (Exception ex)
        {
            return ActionResultHelper.HandleError(_logger, ex, $"An error occurred while fetching category with ID {id}.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveCategory([FromRoute] Guid id)
    {
        try
        {
            var result = await _mediator.Send(new RemoveGategoryCommand { id = id });

            if (!result)
                return ActionResultHelper.HandleNotFound(_logger, $"Category with ID {id} not found for deletion.");

            return ActionResultHelper.HandleSuccess(_logger, $"Category with ID {id} deleted successfully.", result);
        }
        catch (Exception ex)
        {
            return ActionResultHelper.HandleError(_logger, ex, $"An error occurred while deleting category with ID {id}.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return ActionResultHelper.HandleSuccess(_logger, $"Category created successfully with ID: {result.id}", result);
        }
        catch (Exception ex)
        {
            return ActionResultHelper.HandleError(_logger, ex, "An error occurred while creating a category.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryCommand command)
    {
        try
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            if (result == null)
                return ActionResultHelper.HandleNotFound(_logger, $"Category with ID {id} not found for update.");

            return ActionResultHelper.HandleSuccess(_logger, $"Category with ID {id} updated successfully.", result);
        }
        catch (Exception ex)
        {
            return ActionResultHelper.HandleError(_logger, ex, $"An error occurred while updating category with ID {id}.");
        }
    }
}
