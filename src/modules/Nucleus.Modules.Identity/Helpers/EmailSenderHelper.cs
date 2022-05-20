using System.Web;
using Microsoft.Extensions.Configuration;
using Nucleus.Application.Email;
using Nucleus.Domain.AppConstants;
using Nucleus.Domain.Entities.Authorization;

namespace Nucleus.Modules.Identity.Helpers;

// TODO: There are messages that are not localized. Localize them.
public static class EmailSenderHelper
{
    public static async Task SendRegistrationConfirmationMail(IEmailSender emailSender, IConfiguration configuration, User user, string confirmationToken)
    {
        var callbackUrl = $"{configuration[AppConfig.App_ClientUrl]}/identity/confirm-email?email={user.Email}&token={HttpUtility.UrlEncode(confirmationToken)}";
        var subject = "Confirm your email.";
        var message = $"Please confirm your identity by clicking this link: <a href='{callbackUrl}'>{callbackUrl}</a>";
        await emailSender.SendEmailAsync(user.Email, subject, message);
    }

    public static async Task SendForgotPasswordMail(IEmailSender emailSender, IConfiguration configuration, string resetToken, User user)
    {
        var callbackUrl = $"{configuration[AppConfig.App_ClientUrl]}/identity/reset-password?token={HttpUtility.UrlEncode(resetToken)}";
        var subject = "Reset your password.";
        var message = $"Please reset your password by clicking this link: <a href='{callbackUrl}'>{callbackUrl}</a>";

        await emailSender.SendEmailAsync(user.Email, subject, message);
    }
}