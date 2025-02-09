using clean_architecture.Application.Features.Category.Model;
using clean_architecture.Data;
using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Application.Features.Category.Command.AddCategoryCommand;

public class AddCategoryHandler :  IRequestHandler< AddCategoryCommand, CategoryEntity>
{
    private readonly AppDbContext _dbContext;

    public AddCategoryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

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