using clean_architecture.Application.Features.UserInfo.Model;
using MediatR;

namespace clean_architecture.Application.Features.UserInfo.Query;

public class UserInfoQuery : IRequest<UserInfoDto>
{
    public Guid UserId { get; set; }
}
