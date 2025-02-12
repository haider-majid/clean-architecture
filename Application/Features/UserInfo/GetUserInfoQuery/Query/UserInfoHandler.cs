using AutoMapper;
using clean_architecture.Application.Features.UserInfo.Model;
using clean_architecture.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace clean_architecture.Application.Features.UserInfo.Query;

public class UserInfoHandler : BaseHandler , IRequestHandler<UserInfoQuery , UserInfoDto>
{
    public UserInfoHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
    
    public async Task<UserInfoDto> Handle(UserInfoQuery request, CancellationToken cancellationToken)
    {
        var user = _dbContext.users.AsNoTracking();
        var userDto = _mapper.Map<UserInfoDto>(user);

        return userDto;
        
    }
}