using MediatR;

namespace clean_architecture.Commands.CreateProductCommand
{
    public class CreateProductCommand : IRequest<bool>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}