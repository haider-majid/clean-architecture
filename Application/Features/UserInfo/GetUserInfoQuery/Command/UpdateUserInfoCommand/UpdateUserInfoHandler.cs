using AutoMapper;
using clean_architecture.Data;
using MediatR;

namespace clean_architecture.Application.Features.UserInfo.GetUserInfoQuery.Command.UpdateUserInfoCommand;

public class UpdateUserInfoHandler : BaseHandler ,  IRequestHandler<UpdateUserInfoCommand, bool>
{
    public UpdateUserInfoHandler (AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
    public async Task<bool> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.users.FindAsync(request.UserId);
        if (user == null)
        {
            return false;
        }
        user.name = request.name ?? user.name;
        user.email = request.email ?? user.email;
        user.userName = request.userName ?? user.userName;
        user.location = request.location ?? user.location;
        _dbContext.users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
       
    }
}