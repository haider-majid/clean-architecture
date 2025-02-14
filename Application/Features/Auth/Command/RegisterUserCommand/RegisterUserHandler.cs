using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using clean_architecture.Application.Features.Auth.Model;
using clean_architecture.Data;
using clean_architecture.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace clean_architecture.Application.Features.Auth.Command.RegisterUserCommand;

public class RegisterUserHandler : BaseHandler , IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    public RegisterUserHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Check if the user already exists in the database
        var existingUser = await _dbContext.users
            .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

        if (existingUser != null)
        {
            return new RegisterUserResponse
            {
                StatusCode = 400,
                Message = "User already exists."
            };
        }

        // Hash the password before storing it
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var newUser = new UserEntity
        {
            Username = request.Username,
            PasswordHash = hashedPassword,
            Loaction = request.Location
        };

        _dbContext.users.Add(newUser);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new RegisterUserResponse
        {
            StatusCode = 200,
            Message = "User registered successfully.",
            Username = newUser.Username
        };
    }
}