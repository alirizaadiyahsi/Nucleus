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

        Task<bool> IsUserGrantedToPermissionAsync(string userNameOrEmail, string permissionName);

        Task<bool> IsRoleGrantedToPermissionAsync(Role role, Permission permission);
    }
}