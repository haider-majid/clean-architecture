using MediatR;

namespace clean_architecture.Application.Features.Auth.Command.RegisterUserCommand;

public class RegisterUserCommand : IRequest<bool>
{
    public string Username { get; set; }
    public string Password { get; set; }
}