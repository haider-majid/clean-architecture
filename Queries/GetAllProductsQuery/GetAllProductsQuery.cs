using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Queries.GetAllProductsQuery;

public class GetAllProductsQuery : IRequest<List<ProductEntity>>
{
    
}