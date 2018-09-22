using System.Security.Claims;
using System.Threading.Tasks;
using Nucleus.Application.Permissions.Dto;
using Nucleus.Core.Permissions;
using Nucleus.Core.Roles;
using Nucleus.Utilities.Collections;

namespace Nucleus.Application.Permissions
{
    public interface IPermissionAppService
    {
        Task<IPagedList<PermissionListOutput>> GetPermissionsAsync(PermissionListInput input);

        Task<bool> IsPermissionGrantedToUserAsync(ClaimsPrincipal contextUser, Permission permission);

        Task<bool> IsPermissionGrantedToRoleAsync(Role role, Permission permission);

        //todo: grant permission to user
        //todo: grant permission to role
    }
}