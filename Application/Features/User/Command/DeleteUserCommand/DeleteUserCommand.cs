using MediatR;

namespace clean_architecture.Application.Features.Auth.Command.DeleteUserCommand;

public class DeleteUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}