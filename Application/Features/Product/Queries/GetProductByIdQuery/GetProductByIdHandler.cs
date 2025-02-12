using AutoMapper;
using clean_architecture.Data;
using clean_architecture.Entity;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace clean_architecture.Queries.GetProductByIdQuery;

public class GetProductByIdHandler :BaseHandler ,  IRequestHandler<GetProductByIdQuery, ProductEntity>

{

    
    public GetProductByIdHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
    public async Task<ProductEntity> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.products.FindAsync(request.id);
        if (product == null)
        {
            return null;
        }
        return product;
    }
}