using clean_architecture.Data;
using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Commands.UpdateProductCommand;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand , bool>
{
    private readonly AppDbContext _context;

    public UpdateProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productget = await _context.products.FindAsync(request.id);
        productget.Name = request.Name ?? productget.Name;
        productget.Description = request.Description ?? productget.Description;
        _context.products.Update(productget);
        _context.SaveChangesAsync(cancellationToken);
        return true;

    }
}