using AutoMapper;
using clean_architecture.Application.Features.UserInfo.Model;
using clean_architecture.Data;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace clean_architecture.Application.Features.UserInfo.Query;

public class UserInfoHandler : BaseHandler , IRequestHandler<UserInfoQuery , UserInfoDto>
{
    public UserInfoHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
    
    public async Task<UserInfoDto> Handle(UserInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.users.FindAsync(request.UserId);
        var userDto = _mapper.Map<UserInfoDto>(user);
        
        if (user == null)
        {
            return null;
        }
        return userDto;

    }
}