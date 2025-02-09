using AutoMapper;
using clean_architecture.Application.Features.Category.Model;
using clean_architecture.Data;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace clean_architecture.Application.Features.Category.Query.GetCategoryQuery;

public class GetCategoryHandler : BaseHandler ,  IRequestHandler<GetCategoryQuery, CategoryDto>
{

    public GetCategoryHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
    

    public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        if (_dbContext == null)
            throw new InvalidOperationException("Database context is not initialized.");

        var category = await _dbContext.categories.FindAsync(request.Id);
        var categoryDto = _mapper.Map<CategoryDto>(category);
        return categoryDto;
    }
}
