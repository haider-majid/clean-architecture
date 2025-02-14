namespace clean_architecture.Application.Features.User.Model;

public class UserDto
{
    
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Location { get; set; }
}