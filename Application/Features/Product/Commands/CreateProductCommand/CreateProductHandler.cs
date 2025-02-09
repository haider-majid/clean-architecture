using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using clean_architecture.Data;
using clean_architecture.Entity;

namespace clean_architecture.Commands.CreateProductCommand
{
    public class CreateProductHandler : BaseHandler, IRequestHandler<CreateProductCommand, bool>
    {
       

        public CreateProductHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

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
            _dbContext.products.Add(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true; 
        }
    }
}