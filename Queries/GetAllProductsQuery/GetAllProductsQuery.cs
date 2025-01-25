using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Queries.GetAllProductsQuery;

public class GetAllProductsQuery : IRequest<ProductEntity>
{
    
    public string Name { get; set; }
    
    
}