namespace clean_architecture.Application.Features.Auth.Model;
public class RegisterUserResponse
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public string Username { get; set; }
    public string Location { get; set; }
}
