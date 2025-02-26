using FluentValidation;

namespace clean_architecture.Commands.CreateProductCommand;

public class CreateProductValidate :  AbstractValidator<CreateProductCommand>
{

    public CreateProductValidate()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(20)
            .WithMessage("Name must not exceed 20 characters.");
        
        RuleFor(p => p.Description).MaximumLength(100).WithMessage("Description must not exceed 100 characters.");
    }
}