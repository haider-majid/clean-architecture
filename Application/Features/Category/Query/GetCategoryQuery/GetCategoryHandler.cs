using AutoMapper;
using clean_architecture.Application.Features.Category.Model;
using clean_architecture.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace clean_architecture.Application.Features.Category.Query.GetCategoryQuery;

public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, List<CategoryDto>>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public GetCategoryHandler(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<List<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        
        
        var category = await _dbContext.categories.ToListAsync();
        var categoryDto = _mapper.Map<List<CategoryDto>>(category);
        return categoryDto;

    }
}