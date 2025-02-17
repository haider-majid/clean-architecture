using AutoMapper;
using clean_architecture.Data;
using MediatR;

namespace clean_architecture.Application.Features.Auth.Command.DeleteUserCommand;

public class DeleteUserHandler : BaseHandler, IRequestHandler<DeleteUserCommand, bool>
{
    public DeleteUserHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {

        var user = _dbContext.users.FirstOrDefault(x => x.Id == request.Id);

        if (user == null)
        {
            return null;
        }

        _dbContext.users.Remove(user);
        _dbContext.SaveChanges();
        return Task.FromResult(true);

    }
}