using clean_architecture.Application.Features.User.Model;
using clean_architecture.Entity;
using MediatR;

namespace clean_architecture.Application.Features.User.Query.GetUserInfo;

public class GetUserInfoQuery : IRequest<UserDto>
{
    
    public Guid UserId { get; set; }
    
}