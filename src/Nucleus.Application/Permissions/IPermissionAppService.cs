using System.Threading.Tasks;
using Nucleus.Application.Permissions.Dto;
using Nucleus.Utilities.Collections;

namespace Nucleus.Application.Permissions
{
    public interface IPermissionAppService
    {
        Task<IPagedList<PermissionListOutput>> GetAllPermissionsAsync(PermissionListInput input);

        Task<bool> IsUserGrantedToPermissionAsync(string userNameOrEmail, string permissionName);
    }
}