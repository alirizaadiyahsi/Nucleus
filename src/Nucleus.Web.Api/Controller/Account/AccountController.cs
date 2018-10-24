using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nucleus.Application.Dto;
using Nucleus.Core.Users;
using Nucleus.Web.Api.Models;
using Nucleus.Web.Core.Authentication;
using Nucleus.Web.Core.Controllers;

namespace Nucleus.Web.Api.Controller.Account
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtTokenConfiguration _jwtTokenConfiguration;

        //todo: change DTO names to make it standard (use -input, -output post fix), open an issue related this todo
        public AccountController(
            UserManager<User> userManager,
            IOptions<JwtTokenConfiguration> jwtTokenConfiguration)
        {
            _userManager = userManager;
            _jwtTokenConfiguration = jwtTokenConfiguration.Value;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<LoginResult>> Login([FromBody]LoginViewModel loginViewModel)
        {
            var userToVerify = await CreateClaimsIdentityAsync(loginViewModel.UserNameOrEmail, loginViewModel.Password);
            if (userToVerify == null)
            {
                return BadRequest(
                    new NameValueDto("ErrorMessage", "The user name or password is incorrect."));
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

            return Ok(new LoginResult { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<RegisterResult>> Register([FromBody]RegisterViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return BadRequest(new NameValueDto("EmailAlreadyExist", "This email already exists!"));
            }

            var applicationUser = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(applicationUser, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(e => new NameValueDto(e.Code, e.Description)).ToList());
            }

            //todo: no need to return RegisterResult return just OK and show message at client side
            return Ok(new RegisterResult { ResultMessage = "Your account has been successfully created." });
        }

        private async Task<ClaimsIdentity> CreateClaimsIdentityAsync(string userNameOrEmail, string password)
        {
            if (string.IsNullOrEmpty(userNameOrEmail) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var userToVerify = await _userManager.FindByEmailAsync(userNameOrEmail) ?? await _userManager.FindByNameAsync(userNameOrEmail);

            if (userToVerify == null)
            {
                return null;
            }

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return new ClaimsIdentity(new GenericIdentity(userNameOrEmail, "Token"), new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userNameOrEmail),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                });
            }

            return null;
        }
    }
}