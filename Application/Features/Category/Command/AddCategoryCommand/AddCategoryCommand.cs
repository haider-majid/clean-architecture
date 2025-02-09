using clean_architecture.Application.Features.Category.Model;
using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Application.Features.Category.Command.AddCategoryCommand;

public class AddCategoryCommand : IRequest<CategoryEntity>

{
    public string Name { get; set; }
    
}