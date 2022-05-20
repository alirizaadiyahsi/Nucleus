using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Nucleus.Application.Identity;
using Nucleus.Domain.Entities.Authorization;

namespace Nucleus.Modules.Identity.Helpers;

public static class IdentityHelper
{
    public static async Task<ClaimsIdentity> CreateClaimsIdentityAsync(IIdentityAppService identityAppService, string userNameOrEmail, string password)
    {
        if (string.IsNullOrEmpty(userNameOrEmail) || string.IsNullOrEmpty(password))
        {
            return null;
        }

        var userToVerify = await identityAppService.FindUserByUserNameOrEmailAsync(userNameOrEmail);
        if (userToVerify == null)
        {
            return null;
        }

        if (!await identityAppService.CheckPasswordAsync(userToVerify, password)) return null;
        var claims = CreateUserClaims(userToVerify);
        claims = CreateRoleClaims(identityAppService, userToVerify.UserName, claims);

        return new ClaimsIdentity(new ClaimsIdentity(new GenericIdentity(userNameOrEmail, "Token"), claims));
    }

    private static List<Claim> CreateRoleClaims(IIdentityAppService identityAppService, string userName, List<Claim> claims)
    {
        var roles = identityAppService.GetRolesByUserName(userName);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        return claims;
    }

    private static List<Claim> CreateUserClaims(User userToVerify)
    {
        var claims = new List<Claim>(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userToVerify.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Id", userToVerify.Id.ToString())
        });

        return claims;
    }
}