using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clean_architecture.Application.Features.UserInfo.Model;

public class UserInfoDto
{
    [Key]
    [Column("user_id")] 
    public Guid id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string userName { get; set; }
    public string location { get; set; }
}