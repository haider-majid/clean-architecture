using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Application.Features.Category.Command.UpdateCategoryCommand;

public class UpdateCategoryCommand : IRequest<CategoryEntity>
{
    
      public Guid Id { get; set; }
   public  string Name { get; set; }
    
}