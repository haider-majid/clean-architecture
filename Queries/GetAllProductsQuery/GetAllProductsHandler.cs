using clean_architecture.Data;
using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Queries.GetAllProductsQuery;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery , ProductEntity>
{
    private readonly AppDbContext _db;
    
    public GetAllProductsHandler(AppDbContext db)
    {
        _db = db;
    }
    public Task<ProductEntity> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return null;
    }
}