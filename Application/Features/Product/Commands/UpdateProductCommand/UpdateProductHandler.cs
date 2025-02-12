using AutoMapper;
using clean_architecture.Data;
using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Commands.UpdateProductCommand;

public class UpdateProductHandler : BaseHandler ,  IRequestHandler<UpdateProductCommand , ProductEntity>
{
 

    public UpdateProductHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

    public async Task<ProductEntity> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (request.id == Guid.Empty)
            throw new ArgumentException("Invalid category ID.", nameof(request.id));

        var product = await _dbContext.products.FindAsync(request.id);
        if (product == null)
        {
            return null;
        }

        product.Name = request.Name ?? product.Name;
        product.Description = request.Description ?? product.Description;
        _dbContext.products.Update(product);
        await _dbContext.SaveChangesAsync();

        return product;
    }
}