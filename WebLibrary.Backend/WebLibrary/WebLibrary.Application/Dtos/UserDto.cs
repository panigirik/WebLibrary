using System.ComponentModel.DataAnnotations.Schema;
using WebLibrary.Domain.Enums;

namespace WebLibrary.Application.Dtos;

public class UserDto
{
    public Guid UserId { get; set; }  = Guid.NewGuid();
    public string UserName { get; set; } = "SampleUsername";
    public string Email { get; set; } = "samaplemail@gmail.com";
    public string PasswordHash { get; set; }
    public Roles RoleType { get; set; } = Roles.AdminRole;
    
    [NotMapped]
    public List<Guid> BorrowedBooksIds { get; set; } = new(); 
}