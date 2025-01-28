using MediatR;

namespace clean_architecture.Commands.DeleteProductCommand;

public class DeleteProductCommand : IRequest<bool>
{
    
    public Guid id { set; get; }
    
}