using AutoMapper;
using clean_architecture.Application.Features.Category.Model;
using clean_architecture.Data;
using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Application.Features.Category.Command.AddCategoryCommand;

public class AddCategoryHandler : BaseHandler,   IRequestHandler< AddCategoryCommand, CategoryEntity>
{

    public AddCategoryHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

    public async Task<CategoryEntity> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CategoryEntity
        {
            Name = request.Name
        };
        _dbContext.categories.Add(category);
        await _dbContext.SaveChangesAsync();
        
        return category;
        
    }
}