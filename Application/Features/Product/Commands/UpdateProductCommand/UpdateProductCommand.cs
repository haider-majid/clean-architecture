using System.Text.Json.Serialization;
using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Commands.UpdateProductCommand;

public class UpdateProductCommand : IRequest<ProductEntity>
{
    
    
    [JsonIgnore] // Ignore this field if the body is received as JSON
    public Guid id {get; set;}
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    
}