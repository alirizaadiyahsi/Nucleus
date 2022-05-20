using Microsoft.Extensions.Options;
using Serilog;
using MimeKit;

namespace Nucleus.Application.Email;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(
        IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderUsername, _emailSettings.SenderEmail));
        mimeMessage.To.Add(new MailboxAddress(email, email));
        mimeMessage.Subject = subject;
        mimeMessage.Body = new TextPart("html")
        {
            Text = message
        };

#if DEBUG
        Log.Information(@$"-------- Send Email: From: {_emailSettings.SenderEmail} - To: {email}
                                       \n -------- Subject: {subject}
                                       \n -------- Body: {message}");
#else
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, _emailSettings.EnableSsl);
                await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
#endif
    }
}