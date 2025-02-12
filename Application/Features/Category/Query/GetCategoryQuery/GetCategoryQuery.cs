using clean_architecture.Application.Features.Category.Model;
using MediatR;

namespace clean_architecture.Application.Features.Category.Query.GetCategoryQuery;

public class GetCategoryQuery : IRequest<CategoryDto>
{
    
    public Guid Id { get; set; }
    
}