using AutoMapper;
using clean_architecture.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace clean_architecture.Application.Features.UserInfo.GetUserInfoQuery.Command.UpdateUserInfoCommand;

public class UpdateUserInfoHandler : BaseHandler, IRequestHandler<UpdateUserInfoCommand, bool>
{
    public UpdateUserInfoHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

    public async Task<bool> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.users.FindAsync(request.UserId); // Fixed `users` to `Users`
        if (user == null)
        {
            return false;
        }
        //
        // // Update fields only if they are provided (not null)
        // if (!string.IsNullOrEmpty(request. name)) user.Name = request.name;
        // if (!string.IsNullOrEmpty(request.email)) user.Email = request.email;
        // if (!string.IsNullOrEmpty(request.userName)) user.UserName = request.userName;
        // if (!string.IsNullOrEmpty(request.location)) user.Location = request.location;

        _dbContext.users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}