using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using clean_architecture.Application.Features.Auth.Command.RegisterUserCommand;
using clean_architecture.Entity;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace clean_architecture.Application.Features.Auth.Command.LoginUserCommand;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, AuthResponse>
{
    private static readonly ConcurrentDictionary<string, string> _users = RegisterUserHandler._users;
    private readonly string _jwtKey = "Your_Secret_Key_Here"; // Use the same key from Program.cs

    public Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        if (!_users.TryGetValue(request.Username, out var hashedPassword) ||
            !BCrypt.Net.BCrypt.Verify(request.Password, hashedPassword))
        {
            return Task.FromResult<AuthResponse>(null);
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, request.Username) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Task.FromResult(new AuthResponse { Token = tokenHandler.WriteToken(token) });
    }
}