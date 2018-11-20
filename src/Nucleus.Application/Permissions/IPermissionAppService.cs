using System.Collections.Generic;
using System.Threading.Tasks;
using Nucleus.Application.Permissions.Dto;

namespace Nucleus.Application.Permissions
{
    public interface IPermissionAppService
    {
        Task<IEnumerable<PermissionDto>> GetGrantedPermissionsAsync(string userNameOrEmail);

        Task<bool> IsUserGrantedToPermissionAsync(string userNameOrEmail, string permissionName);
    }
}