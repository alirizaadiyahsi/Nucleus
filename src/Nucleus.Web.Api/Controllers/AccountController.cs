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
using Nucleus.Web.Core.Helpers;

namespace Nucleus.Web.Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtTokenConfiguration _jwtTokenConfiguration;

        public AccountController(
            UserManager<User> userManager,
            IOptions<JwtTokenConfiguration> jwtTokenConfiguration)
        {
            _userManager = userManager;
            _jwtTokenConfiguration = jwtTokenConfiguration.Value;
        }

        //todo: check login by username or email
        [HttpPost("[action]")]
        public async Task<ActionResult<LoginResult>> Login([FromBody]LoginViewModel loginViewModel)
        {
            var userToVerify = await CreateClaimsIdentityAsync(loginViewModel.UserName, loginViewModel.Password);
            if (userToVerify == null)
            {
                return BadRequest(new ErrorResult
                {
                    Errors = ErrorHelper.CreateError("ErrorMessage", "The user name or password is incorrect.")
                });
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
                return BadRequest(new ErrorResult
                {
                    Errors = ErrorHelper.CreateError("EmailAlreadyExist", "This email already exists!")
                });
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
                return BadRequest(new ErrorResult
                {
                    Errors = result.Errors.Select(e => new NameValueDto(e.Code, e.Description)).ToList()
                });
            }

            return Ok(new RegisterResult { ResultMessage = "Your account has been successfully created." });
        }

        private async Task<ClaimsIdentity> CreateClaimsIdentityAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var userToVerify = await _userManager.FindByNameAsync(userName);
            if (userToVerify == null)
            {
                return null;
            }

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                });
            }

            return null;
        }
    }
}