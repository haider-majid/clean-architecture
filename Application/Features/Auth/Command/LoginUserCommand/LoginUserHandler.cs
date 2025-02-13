using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using clean_architecture.Application.Common;
using clean_architecture.Application.Features.Auth.Model;
using clean_architecture.Data;
using clean_architecture.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace clean_architecture.Application.Features.Auth.Command.LoginUserCommand;

public class LoginUserHandler : BaseHandler , IRequestHandler<LoginUserCommand, AuthResponse>
{
   public LoginUserHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
    public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.users
            .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return new AuthResponse
            {
                Token = null,
                Username = null,
                StatusCode = 401,
                Massage = "Invalid username or password."
            };
        }

        // Generate JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(Constants.JWTKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Username) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AuthResponse
        {
            Token = tokenHandler.WriteToken(token),
            Username = user.Username,
            StatusCode = 200,
            Massage = "Login successful."
        };
    }
}
