using clean_architecture.Data;
using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Commands.UpdateProductCommand;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand , ProductEntity>
{
    private readonly AppDbContext _context;

    public UpdateProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProductEntity> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (request.id == Guid.Empty)
            throw new ArgumentException("Invalid category ID.", nameof(request.id));

        var product = await _context.products.FindAsync(request.id);
        if (product == null)
        {
            return null;
        }

        product.Name = request.Name ?? product.Name;
        product.Description = request.Description ?? product.Description;
        _context.products.Update(product);
        await _context.SaveChangesAsync();

        return product;
    }
}