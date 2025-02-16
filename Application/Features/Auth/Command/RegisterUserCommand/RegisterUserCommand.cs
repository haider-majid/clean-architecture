using clean_architecture.Application.Features.Auth.Model;
using MediatR;

namespace clean_architecture.Application.Features.Auth.Command.RegisterUserCommand;

public class RegisterUserCommand : IRequest<RegisterUserResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Location { get; set; }
}