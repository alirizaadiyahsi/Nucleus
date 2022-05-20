using Microsoft.AspNetCore.Identity;
using Nucleus.Domain.AppConstants;
using Nucleus.Domain.Entities.Authorization;

namespace Nucleus.Application.Authorization.Permissions;

public class PermissionAppService : IPermissionAppService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public PermissionAppService(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> IsUserGrantedToPermissionAsync(string userName, string permission)
    {
        var user = await _userManager.FindByNameAsync(userName);
        var userClaims = await _userManager.GetClaimsAsync(user);
        if (userClaims.Any(x => x.Type == AppClaimTypes.Permission && x.Value == permission))
        {
            return true;
        }

        var userRoles = user.UserRoles.Select(ur => ur.Role);
        foreach (var role in userRoles)
        {
            var roleClaims = await _roleManager.GetClaimsAsync(role);
            if (roleClaims.Any(x => x.Type == AppClaimTypes.Permission && x.Value == permission))
            {
                return true;
            }
        }

        return false;
    }
}