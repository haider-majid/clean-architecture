using clean_architecture.Data;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace clean_architecture.Application.Features.Category.Command.RemoveGategoryHandler;

public class RemoveGategoryHandler : IRequestHandler<RemoveGategoryCommand.RemoveGategoryCommand , bool>
{
    private readonly AppDbContext _dbContext;

    public RemoveGategoryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<bool> Handle(RemoveGategoryCommand.RemoveGategoryCommand request, CancellationToken cancellationToken)
    {
        
        var category =  _dbContext.categories.FirstOrDefault(x => x.id ==  request.id);
        if (category == null)
        {
            return false; 
        }
        _dbContext.categories.Remove(category);
      
        await _dbContext.SaveChangesAsync(cancellationToken:cancellationToken);
        return true;
        
    }
}