using AutoMapper;
using clean_architecture.Data;
using clean_architecture.Entity;
using clean_architecture.Features.Product.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace clean_architecture.Queries.GetAllProductsQuery;

public class GetAllProductsQueryHandler : BaseHandler ,  IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{


    public GetAllProductsQueryHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        // Fetch all products from the database
        var products = await _dbContext.products
            .AsNoTracking()
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync(cancellationToken);
        // Map ProductEntity to ProductDto using AutoMapper
        var productDtos = _mapper.Map<List<ProductDto>>(products);

        return productDtos;
    }
}