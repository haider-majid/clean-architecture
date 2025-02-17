using System.Text.Json.Serialization;
using MediatR;

namespace clean_architecture.Application.Features.User.Command.UpdateUserCommand;

public class UpdateUserCommand : IRequest<bool>
{
    
    
    [JsonIgnore]
    public Guid id { get; set; }
    public string Username { get; set; }
    
    public string Location { get; set; }
    
}