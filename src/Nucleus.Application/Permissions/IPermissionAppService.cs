using System.Collections.Generic;
using System.Threading.Tasks;
using Nucleus.Application.Permissions.Dto;
using Nucleus.Core.Permissions;

namespace Nucleus.Application.Permissions
{
    public interface IPermissionAppService
    {
        Task<IEnumerable<PermissionDto>> GetGrantedPermissionsAsync(string userNameOrEmail);

        Task<bool> IsUserGrantedToPermissionAsync(string userNameOrEmail, string permissionName);

        void InitializePermissions(List<Permission> permissions);
    }
}