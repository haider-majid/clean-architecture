using System.Text.Json.Serialization;
using AutoMapper.Configuration.Annotations;
using MediatR;

namespace clean_architecture.Application.Features.UserInfo.GetUserInfoQuery.Command.UpdateUserInfoCommand;

public class UpdateUserInfoCommand : IRequest<bool>
{
    
    
    [JsonIgnore]
    public Guid UserId { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string userName { get; set; }
    public string location { get; set; }
    
}