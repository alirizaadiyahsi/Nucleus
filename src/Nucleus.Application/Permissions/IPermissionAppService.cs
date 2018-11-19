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

        Task<bool> IsUserGrantToPermissionAsync(string userNameOrEmail, string permissionName);

        Task<bool> IsRoleGrantToPermissionAsync(Role role, Permission permission);
    }
}