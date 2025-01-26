using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Queries.GetProductByIdQuery;

public class GetProductByIdQuery : IRequest<ProductEntity>
{
    public  Guid id { get; set; }
    
}