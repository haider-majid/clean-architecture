using clean_architecture.Entity;
using clean_architecture.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace clean_architecture.Queries.GetAllProductsQuery;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductEntity>>
{
    private readonly AppDbContext _dbContext;

    public GetAllProductsQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ProductEntity>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.products.AsQueryable();
        return await query.ToListAsync(cancellationToken);
    }
}