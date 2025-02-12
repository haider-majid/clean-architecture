using System.Collections.Concurrent;
using MediatR;

namespace clean_architecture.Application.Features.Auth.Command.RegisterUserCommand;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, bool>
{
    internal static readonly ConcurrentDictionary<string, string> _users = new();

    public Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (_users.ContainsKey(request.Username))
            return Task.FromResult(false);

        _users[request.Username] = BCrypt.Net.BCrypt.HashPassword(request.Password);
        return Task.FromResult(true);
    }
}