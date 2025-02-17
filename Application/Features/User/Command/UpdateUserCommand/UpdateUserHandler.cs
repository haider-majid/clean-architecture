using AutoMapper;
using clean_architecture.Data;
using MediatR;

namespace clean_architecture.Application.Features.User.Command.UpdateUserCommand;

public class UpdateUserHandler : BaseHandler , IRequestHandler<UpdateUserCommand , bool>
{

    public UpdateUserHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper){}
    
    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user =await _dbContext.users.FindAsync( request.id);
        if (user == null)
        {
            return false;
        }
        user.Username = request.Username ?? user.Username;
        user.Location = request.Location ?? user.Location;
        _dbContext.users.Update(user);
        _dbContext.SaveChanges();
        return true;
    }
}