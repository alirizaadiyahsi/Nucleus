using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nucleus.Application.Email;
using Nucleus.Application.Identity;
using Nucleus.Application.Identity.Dto;
using Nucleus.Domain.Entities.Authorization;
using Nucleus.Modules.Identity.Helpers;
using Nucleus.Web.Core;
using Nucleus.Web.Core.Configuration;
using Nucleus.Web.Core.Controllers;

namespace Nucleus.Modules.Identity.Controllers;

[Route("api/[module]")]
public class IdentityController : ApiControllerBase
{
    private readonly IIdentityAppService _identityAppService;
    private readonly JwtTokenConfiguration _jwtTokenConfiguration;
    private readonly IConfiguration _configuration;
    private readonly IEmailSender _emailSender;

    public IdentityController(
        IIdentityAppService identityAppService,
        IOptions<JwtTokenConfiguration> jwtTokenConfiguration,
        IConfiguration configuration, 
        IEmailSender emailSender)
    {
        _identityAppService = identityAppService;
        _configuration = configuration;
        _emailSender = emailSender;
        _jwtTokenConfiguration = jwtTokenConfiguration.Value;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<LoginOutput>> Login([FromBody]LoginInput input)
    {
        var userToVerify = await IdentityHelper.CreateClaimsIdentityAsync(_identityAppService, input.UserNameOrEmail, input.Password);
        if (userToVerify == null)
        {
            return BadRequest(UserFriendlyMessages.UserNameOrPasswordNotCorrect);
        }

        var token = new JwtSecurityToken
        (
            issuer: _jwtTokenConfiguration.Issuer,
            audience: _jwtTokenConfiguration.Audience,
            claims: userToVerify.Claims,
            expires: _jwtTokenConfiguration.EndDate,
            notBefore: _jwtTokenConfiguration.StartDate,
            signingCredentials: _jwtTokenConfiguration.SigningCredentials
        );

        return Ok(new LoginOutput
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> Register([FromBody]RegisterInput input)
    {
        var user = await _identityAppService.FindUserByEmailAsync(input.Email);
        if (user != null) return Conflict(UserFriendlyMessages.EmailAlreadyExist);

        user = await _identityAppService.FindUserByUserNameAsync(input.UserName);
        if (user != null) return Conflict(UserFriendlyMessages.UserNameAlreadyExist);

        var applicationUser = new User
        {
            UserName = input.UserName,
            Email = input.Email
        };

        var result = await _identityAppService.CreateUserAsync(applicationUser, input.Password);

        if (!result.Succeeded)
        {
            return BadRequest(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
        }

        // TODO: Email confirmation is disabled for now
        // var confirmationToken = await _identityAppService.GenerateEmailConfirmationTokenAsync(applicationUser);
        // await EmailSenderHelper.SendRegistrationConfirmationMail(_emailSender, _configuration, applicationUser, confirmationToken);

        return Ok(new RegisterOutput { ConfirmRegistrationToken = "confirmationToken" });
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmailInput input)
    {
        var user = await _identityAppService.FindUserByEmailAsync(input.Email);
        if (user == null) return NotFound(UserFriendlyMessages.EmailIsNotFound);

        var result = await _identityAppService.ConfirmEmailAsync(user, input.Token);
        if (!result.Succeeded) return BadRequest(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));

        return Ok();
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordInput input)
    {
        if (input.NewPassword != input.PasswordRepeat)
        {
            return BadRequest(UserFriendlyMessages.PasswordsAreNotMatched);
        }

        var user = await _identityAppService.FindUserByUserNameAsync(User.Identity?.Name);
        var result = await _identityAppService.ChangePasswordAsync(user, input.CurrentPassword, input.NewPassword);
        if (!result.Succeeded) return BadRequest(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));

        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<ForgotPasswordOutput>> ForgotPassword([FromBody] ForgotPasswordInput input)
    {
        var user = await _identityAppService.FindUserByEmailAsync(input.Email);
        if (user == null) return NotFound(UserFriendlyMessages.UserIsNotFound);

        var resetToken = await _identityAppService.GeneratePasswordResetTokenAsync(user);
        await EmailSenderHelper.SendForgotPasswordMail(_emailSender, _configuration, resetToken, user);

        return Ok(new ForgotPasswordOutput { ResetToken = resetToken });
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordInput input)
    {
        var user = await _identityAppService.FindUserByUserNameOrEmailAsync(input.UserNameOrEmail);
        if (user == null) return NotFound(UserFriendlyMessages.UserIsNotFound);

        var result = await _identityAppService.ResetPasswordAsync(user, input.Token, input.Password);
        if (!result.Succeeded) return BadRequest(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));

        return Ok();
    }
}