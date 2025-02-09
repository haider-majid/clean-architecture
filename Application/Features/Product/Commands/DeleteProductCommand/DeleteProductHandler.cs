using clean_architecture.Data;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace clean_architecture.Commands.DeleteProductCommand;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand , bool >
{
    private readonly AppDbContext _context;

    public DeleteProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.products.FindAsync(request.id);
        if (product == null)
        {
            return false;

        }

        _context.products.Remove(product);
        _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}