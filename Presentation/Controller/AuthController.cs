using clean_architecture.Application.Features.Auth.Command.LoginUserCommand;
using clean_architecture.Application.Features.Auth.Command.RegisterUserCommand;
using clean_architecture.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRequest request)
    {
        var result = await _mediator.Send(new RegisterUserCommand
        {
            Username = request.Username,
            Password = request.Password
        });

        if (result == null)
            return BadRequest("User already exists.");

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequest request)
    {
        var response = await _mediator.Send(new LoginUserCommand
        {
            Username = request.Username,
            Password = request.Password
            
        });

        if (response == null)
            return Unauthorized("Invalid credentials.");

        return Ok(response);
    }
}