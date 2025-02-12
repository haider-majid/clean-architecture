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
        var query = _dbContext.products.AsNoTracking();

        
        
        // Apply search filter if Search is not empty
        if (!string.IsNullOrEmpty(request.Search))
        {
            string lowerSearch = request.Search.ToLower(); 
            query = query.Where(x => x.Name.ToLower().Contains(lowerSearch)); 
        }
        // Map ProductEntity to ProductDto using AutoMapper
        var productDtos = _mapper.Map<List<ProductDto>>(query);

        return productDtos;
    }
}