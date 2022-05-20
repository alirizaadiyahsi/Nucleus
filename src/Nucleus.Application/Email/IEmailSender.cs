namespace Nucleus.Application.Email;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
}