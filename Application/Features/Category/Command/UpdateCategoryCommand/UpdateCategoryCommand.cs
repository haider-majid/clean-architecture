using System.Text.Json.Serialization;
using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Application.Features.Category.Command.UpdateCategoryCommand;

public class UpdateCategoryCommand : IRequest<CategoryEntity>
{
    
    [JsonIgnore] // Ignore this field if the body is received as JSON
    public Guid Id { get; set; }
   public  string Name { get; set; }
    
}