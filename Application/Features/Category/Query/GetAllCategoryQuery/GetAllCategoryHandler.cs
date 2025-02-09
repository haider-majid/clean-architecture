using AutoMapper;
using clean_architecture.Application.Features.Category.Model;
using clean_architecture.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace clean_architecture.Application.Features.Category.Query.GetCategoryQuery;

public class GetAllCategoryHandler : BaseHandler ,  IRequestHandler<GetAllCategoryQuery, List<CategoryDto>>
{
   
    public GetAllCategoryHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

    public async Task<List<CategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        
        var category = await _dbContext.categories.ToListAsync();
        var categoryDto = _mapper.Map<List<CategoryDto>>(category);
        return categoryDto;

    }
}