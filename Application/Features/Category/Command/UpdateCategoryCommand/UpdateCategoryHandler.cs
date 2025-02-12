using AutoMapper;
using clean_architecture.Data;
using clean_architecture.Entity;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace clean_architecture.Application.Features.Category.Command.UpdateCategoryCommand;

public class UpdateCategoryHandler : BaseHandler ,  IRequestHandler<UpdateCategoryCommand, CategoryEntity>
{


    public UpdateCategoryHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

    public async Task<CategoryEntity> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (request.Id == Guid.Empty)
            throw new ArgumentException("Invalid category ID.", nameof(request.Id));

        var category = await _dbContext.categories.FindAsync(request.Id);
        if (category == null)
        {
            return null;
        }

        category.Name = request.Name ?? category.Name;
        _dbContext.categories.Update(category);
        await _dbContext.SaveChangesAsync();

        return category;
    }
}


