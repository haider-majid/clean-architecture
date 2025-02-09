using AutoMapper;
using clean_architecture.Data;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace clean_architecture.Commands.DeleteProductCommand;

public class DeleteProductHandler : BaseHandler ,  IRequestHandler<DeleteProductCommand , bool >
{

    public DeleteProductHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.products.FindAsync(request.id);
        if (product == null)
        {
            return false;

        }

        _dbContext.products.Remove(product);
        _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}