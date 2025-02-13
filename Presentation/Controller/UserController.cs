using clean_architecture.Application.Features.UserInfo.GetUserInfoQuery.Command.UpdateUserInfoCommand;
using clean_architecture.Application.Features.UserInfo.Query;
using clean_architecture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clean_architecture.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    
    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;
    
    public UserController(IMediator mediator, ILogger<UserController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        try
        {
            var user = await _mediator.Send(new UserInfoQuery { UserId = id });
 
            if (user == null )
                return ActionResultHelper.HandleNotFound(_logger, "No user found.");

            return ActionResultHelper.HandleSuccess(_logger, $"Successfully retrieved {user} user.", user);
        }
        catch (Exception ex)
        {
            return ActionResultHelper.HandleError(_logger, ex, "An error occurred while fetching user.");
        }
    }
    
       
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id , [FromBody] UpdateUserInfoCommand command)
    {
        try
        {
            command.UserId = id;
            var user = await _mediator.Send(command);

            if (user == null )
                return ActionResultHelper.HandleNotFound(_logger, "No user found.");

            return ActionResultHelper.HandleSuccess(_logger, $"Successfully retrieved {user} user.", user);
        }
        catch (Exception ex)
        {
            return ActionResultHelper.HandleError(_logger, ex, "An error occurred while fetching user.");
        }
    }
    
}