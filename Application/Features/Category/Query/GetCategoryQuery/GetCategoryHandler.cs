using AutoMapper;
using clean_architecture.Application.Features.Category.Model;
using clean_architecture.Data;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace clean_architecture.Application.Features.Category.Query.GetCategoryQuery;

public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, CategoryDto>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCategoryHandler(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        if (_dbContext == null)
            throw new InvalidOperationException("Database context is not initialized.");

        var category = await _dbContext.categories.FindAsync(request.Id);
        
        if (category == null)
            throw new KeyNotFoundException($"Category with ID {request.Id} not found.");

        var categoryDto = _mapper.Map<CategoryDto>(category);
        return categoryDto;
    }
}
