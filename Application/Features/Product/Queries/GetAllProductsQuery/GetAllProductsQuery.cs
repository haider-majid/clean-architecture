using clean_architecture.Entity;
using clean_architecture.Features.Product.Models;
using MediatR;

namespace clean_architecture.Queries.GetAllProductsQuery;

public class GetAllProductsQuery : IRequest<List<ProductDto>>
{
    
}