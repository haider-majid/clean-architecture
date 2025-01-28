using MediatR;
using System.Threading;
using System.Threading.Tasks;
using clean_architecture.Data;
using clean_architecture.Entity;

namespace clean_architecture.Commands.CreateProductCommand
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly AppDbContext _context;

        public CreateProductHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Product name is required.");

            // Create entity
            var product = new ProductEntity
            {
                Name = request.Name,
                Description = request.Description
            };

            // Save to database
            _context.products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            return true; 
        }
    }
}