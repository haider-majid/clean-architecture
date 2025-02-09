using clean_architecture.Data;
using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Application.Features.Category.Command.UpdateCategoryCommand;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryEntity>
{
    private readonly AppDbContext _dbContext;

    public UpdateCategoryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<CategoryEntity> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (request.Id == Guid.Empty)
            throw new ArgumentException("Invalid category ID.", nameof(request.Id));

        var category = await _dbContext.categories.FindAsync(request.Id);
        if (category == null)
            throw new KeyNotFoundException($"Category with ID {request.Id} not found.");

        category.Name = request.Name ?? category.Name;
        _dbContext.categories.Update(category);
        await _dbContext.SaveChangesAsync();

        return category;
    }

}
