using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserEntity
{
    
    [Key]
    [Column("user_id")] 
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}