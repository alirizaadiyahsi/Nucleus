namespace Nucleus.Application.Authorization.Permissions;

public interface IPermissionAppService
{
    Task<bool> IsUserGrantedToPermissionAsync(string userName, string permission);
}