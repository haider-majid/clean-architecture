namespace clean_architecture.Application.Features.UserInfo.Model;

public class UserInfoDto
{
    
    public Guid id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string userName { get; set; }
    public string location { get; set; }
}