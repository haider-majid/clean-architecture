using System.Collections.Concurrent;
using clean_architecture.Application.Features.Auth.Model;
using MediatR;

namespace clean_architecture.Application.Features.Auth.Command.RegisterUserCommand;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    public static readonly Dictionary<string, string> _users = new(); 

    public Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (_users.ContainsKey(request.Username))
        {
            return Task.FromResult(new RegisterUserResponse
            {
                Message = "User already exists.",
                StatusCode = 400,
                Username = request.Username
            });
        }

        _users[request.Username] = BCrypt.Net.BCrypt.HashPassword(request.Password);

        return Task.FromResult(new RegisterUserResponse
        {
            Message = "User registered successfully.",
            StatusCode = 200,
            Username = request.Username
        });
    }
}
