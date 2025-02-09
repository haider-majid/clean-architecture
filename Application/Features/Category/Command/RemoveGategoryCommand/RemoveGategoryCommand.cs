using MediatR;

namespace clean_architecture.Application.Features.Category.Command.RemoveGategoryCommand;

public class RemoveGategoryCommand : IRequest<bool>
{
    public Guid id { set;  get; }
    
}