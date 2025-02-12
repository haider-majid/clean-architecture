using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Application.Features.Auth.Command.LoginUserCommand;

public class LoginUserCommand : IRequest<AuthResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}