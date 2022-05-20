namespace Nucleus.Application.Identity.Dto;

public class ConfirmEmailInput
{
    public string Email { get; set; }

    public string Token { get; set; }
}