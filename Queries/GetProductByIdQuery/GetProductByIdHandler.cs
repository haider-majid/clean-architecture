using clean_architecture.Data;
using clean_architecture.Entity;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace clean_architecture.Queries.GetProductByIdQuery;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductEntity>

{
    private readonly AppDbContext _dbContext;
    
    public GetProductByIdHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductEntity> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.products.FindAsync(request.id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {request.id} not found.");
        }
        return product;
    }
}