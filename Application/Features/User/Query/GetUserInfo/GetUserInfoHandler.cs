using AutoMapper;
using clean_architecture.Application.Features.User.Model;
using clean_architecture.Data;
using clean_architecture.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace clean_architecture.Application.Features.User.Query.GetUserInfo;

public class GetUserInfoHandler : BaseHandler ,  IRequestHandler<GetUserInfoQuery, UserDto>
{
    public GetUserInfoHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
    public async Task<UserDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
       var userInfo = await _dbContext.users.FindAsync(request.UserId);
       
         if(userInfo == null)
         {
             return null;
         }
         
         var  user = _mapper.Map<UserDto>(userInfo);
      
         return user;
    }
}