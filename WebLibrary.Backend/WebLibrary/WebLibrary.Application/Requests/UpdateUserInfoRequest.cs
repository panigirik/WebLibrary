using WebLibrary.Domain.Enums;

namespace WebLibrary.Application.Requests;

public class UpdateUserInfoRequest
{
    public Guid UserId { get; set; }
    
    public string UserName { get; set; } = "SampleUsername";
    
    public string Email { get; set; } = "samaplemail@gmail.com";

    public Roles RoleType { get; set; } = Roles.Admin;
}