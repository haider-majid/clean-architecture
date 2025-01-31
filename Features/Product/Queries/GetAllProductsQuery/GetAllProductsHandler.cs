using AutoMapper;
using clean_architecture.Data;
using clean_architecture.Entity;
using clean_architecture.Features.Product.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace clean_architecture.Queries.GetAllProductsQuery;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        // Fetch all products from the database
        var products = await _dbContext.products.ToListAsync(cancellationToken);

        // Map ProductEntity to ProductDto using AutoMapper
        var productDtos = _mapper.Map<List<ProductDto>>(products);

        return productDtos;
    }
}