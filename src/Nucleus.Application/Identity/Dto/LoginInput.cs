namespace Nucleus.Application.Identity.Dto;

public class LoginInput
{
    public string UserNameOrEmail { get; set; }

    public string Password { get; set; }
}