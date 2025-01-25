using clean_architecture.Entity;
using FluentValidation;

namespace clean_architecture.Queries.GetAllProductsQuery;

public class GetAllProductsValidation : AbstractValidator<ProductEntity>
{
    
   public GetAllProductsValidation()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("The name field is required.")
            .MaximumLength(20).WithMessage("The name must not exceed 20 characters.");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("The author field is required.");
        
    }
    
}