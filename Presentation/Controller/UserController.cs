using clean_architecture.Application.Features.User.Query.GetUserInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace clean_architecture.Controllers;


[Route("api/v1/user")]
[ApiController]
public class UserController: ControllerBase
{
    
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserInfo([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetUserInfoQuery
        {
            UserId = id
        });

        if (result == null)
            return NotFound("User not found.");

        return Ok(result);
    }

}