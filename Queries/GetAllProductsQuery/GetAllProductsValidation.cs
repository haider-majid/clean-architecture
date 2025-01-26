using FluentValidation;

namespace clean_architecture.Queries.GetAllProductsQuery;

public class GetAllProductsQueryValidator : AbstractValidator<GetAllProductsQuery>
{
    public GetAllProductsQueryValidator()
    {
        // RuleFor(query => query.Name).Empty()
        //     .MaximumLength(50)
        //     .WithMessage("Name must not exceed 50 characters.");
    }
} 